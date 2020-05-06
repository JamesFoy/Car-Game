using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using XInputDotNetPure;

public class CarController: MonoBehaviour
{
    #region Variable Setup
    public AbilityCoolDown ability; //Reference to the UI ability (needs to be called when a button is pressed)
    public GameObject abilityUIImage; //Reference to the game object that contains the ability UI (makes sure that it only works when activated, activation when picking up powerup occurs etc)

    public ControllerSetup controllerSetup;

    public Transform centerMass; //Used as the transform position for the center of mass

    public enum PlayerNumber { p1, p2 }; //Enum setup for setting players, currently only p1 works
    public PlayerNumber thisNumber;

    [SerializeField, Space(10)]
    public UnityEvent triggerCamShake; //UnityEvent that is setup in editor to raise the camera shake event

    //Creating a list of possible abilites/powerups
    public List<AbilityHolder> possibleAbilities;
    public AbilityHolder abilityTypeChosen;
    public PowerupAbility powerup1;
    public PowerupAbility powerup2;
    public ProjectileAbility projectile1;
    public ProjectileAbility projectile2;

    Rigidbody rb; //Reference to the rigidbody

    public GameObject speedEffect;

    //TEXT VARIABLES USED FOR DEBUGING THE COMPRESSION AMOUNTS FOR EACH RAY
    //[SerializeField]
    //Text rayPoint1Text, rayPoint2Text, rayPoint3Text, rayPoint4Text;

    [SerializeField, Header("Ray Points")] Transform rayPoint1;
    [SerializeField] Transform rayPoint2;
    [SerializeField] Transform rayPoint3;
    [SerializeField] Transform rayPoint4; //References to each transform to cast each ray from

    float dis, actualDistance; //Variables used in compression calculation

    float compressionRatio; //Varibale that contains the exact amount of compression being applied (is clamped between 0 and 1 later in script)

    bool onLand; //Bool used to check if landing (currently used to play the camera shake anytime the car lands after being in the air)

    CarInfo carInfo;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        controllerSetup = GetComponent<ControllerSetup>();
        rb = GetComponent<Rigidbody>();
        abilityUIImage.SetActive(false);
        carInfo = GetComponent<CarInfo>();

        //Setting a center mass removes colliders from calulation of mass (MEANING WE CAN SET COLLIDERS FREELY NOW!!!)
        rb.centerOfMass = new Vector3(centerMass.transform.localPosition.x, centerMass.transform.localPosition.y, centerMass.transform.localPosition.z); //Sets the center of mass for the car based on the local position of the COM transform.
    }

    #region Update Calls
    //Setting up the HUD speed text to a clamped speed varibale (this is based of the speed).
    private void Update()
    {
        float clampedSpeed = Mathf.Clamp(carInfo.carStats.speed, 0, carInfo.carStats.maxSpeed);

        if (clampedSpeed >= 100f)
        {
            speedEffect.SetActive(true);
        }
        else
        {
            speedEffect.SetActive(false);
        }
    }

    // Fixed Update is needed for all following code due to it being physics based (helps to keep everything smooth)
    void FixedUpdate()
    {
        #region Player controls and movement
        //player movement controls
        float forward = 0;
        float backward = 0;
        float turn = 0;
        #region Player 1 Controls
        if (thisNumber == PlayerNumber.p1) //This is setup in the editor to be p1
        {
            #region Controller
            if (controllerSetup.state1.IsConnected)
            {
                forward = controllerSetup.state1.Triggers.Right;
                backward = controllerSetup.state1.Triggers.Left;
                turn = controllerSetup.state1.ThumbSticks.Left.X;
                //Activates the ability
                if (controllerSetup.state1.Buttons.RightShoulder == ButtonState.Pressed)
                {
                    AbilityTrigger(0);
                }
                if (controllerSetup.state1.Buttons.LeftShoulder == ButtonState.Pressed)
                {
                    AbilityTrigger(1);
                }
            }
            #endregion
            #region Keyboard
            else
            {
                forward = Input.GetAxis("Vertical1"); //Setting forward float to be equal to the vertical input (W and S)
                backward = -Input.GetAxis("Vertical1");
                turn = Input.GetAxis("Horizontal1");
                //Activates the ability
                if (Input.GetKey(KeyCode.Space))
                {
                    //AbilityTrigger();
                }
            }
            #endregion
        }
        #endregion
        #region Player 2 Controls
        else if (thisNumber == PlayerNumber.p2)
        {
            #region Controller
            if (controllerSetup.state2.IsConnected)
            {
                forward = controllerSetup.state2.Triggers.Right;
                backward = controllerSetup.state2.Triggers.Left;
                turn = controllerSetup.state2.ThumbSticks.Left.X;
                //Activates the ability
                if (controllerSetup.state2.Buttons.RightShoulder == ButtonState.Pressed)
                {
                    //AbilityTrigger();
                }
            }
            #endregion
            #region Keyboard
            else
            {
                forward = Input.GetAxis("Vertical2"); //Setting forward float to be equal to the vertical input (W and S)
                backward = -Input.GetAxis("Vertical2");
                turn = Input.GetAxis("Horizontal2");
                //Activates the ability
                if (Input.GetKey(KeyCode.KeypadEnter))
                {
                    //AbilityTrigger();
                }
            }
            #endregion
        }
        #endregion
        carInfo.carStats.speed = Mathf.Lerp(carInfo.carStats.speed, carInfo.carStats.maxSpeed, forward * Time.deltaTime / 1f);
        carInfo.carStats.speed = Mathf.Lerp(carInfo.carStats.speed, carInfo.carStats.maxSpeed, backward * Time.deltaTime / 1f);
        ForwardMovement(forward);
        BackwardMovement(backward);
        IdleMovement(forward);
        TurningMovement(turn);
        #endregion 

        #region Debug Text Setup For Compression (Currently not being used)
        //DEBUG SETUP TO SEE COMPRESSION AMOUNTS IN EDITOR
        //rayPoint1Text.text = "" + compressionRatio;
        //rayPoint2Text.text = "" + compressionRatio;
        //rayPoint3Text.text = "" + compressionRatio;
        //rayPoint4Text.text = "" + compressionRatio;
        #endregion

        #region Raycast Setups
        //Debug raycasts for each ray so that you can see them in the editor (had to be .1 less the actual amount to correctly represent length or rays)
        Debug.DrawRay(rayPoint1.position, rayPoint1.forward * 0.5f, Color.red);
        Debug.DrawRay(rayPoint2.position, rayPoint2.forward * 0.5f, Color.red);
        Debug.DrawRay(rayPoint3.position, rayPoint3.forward * 0.5f, Color.red);
        Debug.DrawRay(rayPoint4.position, rayPoint4.forward * 0.5f, Color.red);

        RaycastHit hit; //Defualt hit variable for raycasting

        // Does the ray intersect any objects (this could be changed in the futre to have layer masking however currently isnt needed)
        // Each ray is shot from a raycast point
        if (Physics.Raycast(rayPoint1.position, rayPoint1.forward * 0.6f, out hit))
        {
            CalculateCompression(hit, rayPoint1);
        }
        if (Physics.Raycast(rayPoint2.position, rayPoint2.forward * 0.6f, out hit))
        {
            CalculateCompression(hit, rayPoint2);
        }
        if (Physics.Raycast(rayPoint3.position, rayPoint3.forward * 0.6f, out hit))
        {
            CalculateCompression(hit, rayPoint3);
        }
        if (Physics.Raycast(rayPoint4.position, rayPoint4.forward * 0.6f, out hit))
        {
            CalculateCompression(hit, rayPoint4);
        }
        #endregion

        #region In Air Checks & Landing Setup
        //This is used to check if the car is currently in the air (if compression = 0 it means nothing is below the car so IN AIR!!)
        if (compressionRatio == 0)
        {
            onLand = false;

            //Lowering the speed value while the car is in the air as the player shouldnt be able to effect the cars movement well in air
            carInfo.carStats.speed = 10;

            //Setting the correct rigidbody settings for when the car is in the air, makes sure gravity has more effect etc (can be tweaked to get different resultss)
            rb.mass = 4f;
            rb.drag = 1;
            rb.angularDrag = 5;
        }
        else
        {
            //Check if the player lands and if so invoke the unityevent that shakes the camera
            if (!onLand)
            {
                onLand = true;
                triggerCamShake.Invoke();
            }

            //Reseting the rigidbody values for when the car lands 
            rb.mass = 1.28f;
            rb.drag = 4;
            rb.angularDrag = 10;
        }
        #endregion
    }
    #endregion

    #region Methods/Functions
    //Method used to calcualte the amount of compression to be applied at each corner of the car (each ray inputs its hit information and ray location so the correct force can be applied to each corner)
    void CalculateCompression(RaycastHit hitPoint, Transform originPoint)
    {
        dis = Vector3.Distance(hitPoint.point, originPoint.transform.position) * 2; //"dis" varible is how far from the ground the ray hit point currrently is relative to the starting postion of the ray 
        float clampedDistance = Mathf.Clamp(dis, 0, 1); //Clamps the dis varibale between 0 and 1
        actualDistance = (float)System.Math.Round(clampedDistance, 1); //Round the distance to 1 decimal place
        compressionRatio = 1.0f - actualDistance; //Flips the value of rounded distance (THIS WAS NEEDED AS I WANTED 0 TO BE WHEN NO COMPRESSION WAS BEING APPLIED)... makes sure the correct amount of compression is added based on how far from the ground the ray is

        //Debug.Log(compressionRatio);

        //This applies the force at the exact position of the raycasts
        rb.AddForceAtPosition(transform.up * compressionRatio * carInfo.carStats.suspensionAmount, originPoint.transform.position, ForceMode.Force);

        //Applies a counter force that is slightly lower compression to each point to reduce the effect of compression (HELPS TO MAKE SURE THE CAR DOESN'T BOUCNCE OR PING OFF INTO SPACE)
        rb.AddForceAtPosition(-transform.up * (compressionRatio - 1.0f), originPoint.transform.position, ForceMode.Force);
    }

    //THIS METHOD IS CALLED USEING THE PICKUP GAME EVENT (EVENT IS RAISED WHENEVER A PLAYER COLLIDES WITH A PICKUP)
    //Method used to pick a random ability from the ability list, currently only the rocket will be chosen (also in the furture can add a timer and play an animation on the ability icon to show a roulette sort of randomising)
    public void RandomPickupGenerator()
    {
        abilityTypeChosen = possibleAbilities[UnityEngine.Random.Range(0, possibleAbilities.Count)];

        if (abilityTypeChosen.abilityName == "NanoBotAbility")
        {
            powerup1 = abilityTypeChosen.powerupAbilities[0];
            powerup2 = abilityTypeChosen.powerupAbilities[1];
            abilityUIImage.GetComponent<AbilityCoolDown>().Initialize1(powerup1, abilityUIImage.GetComponent<AbilityCoolDown>().weaponHolder1);
            abilityUIImage.GetComponent<AbilityCoolDown>().Initialize2(powerup2, abilityUIImage.GetComponent<AbilityCoolDown>().weaponHolder2);
        }
        else if (abilityTypeChosen.abilityName == "ExplosiveAbility")
        {
            projectile1 = abilityTypeChosen.projectileAbilities[0];
            projectile2 = abilityTypeChosen.projectileAbilities[1];
            abilityUIImage.GetComponent<AbilityCoolDown>().Initialize1(projectile1, abilityUIImage.GetComponent<AbilityCoolDown>().weaponHolder1);
            abilityUIImage.GetComponent<AbilityCoolDown>().Initialize2(projectile2, abilityUIImage.GetComponent<AbilityCoolDown>().weaponHolder2);
        }
        else if (abilityTypeChosen.abilityName == "ElectricAbility")
        {
            projectile1 = abilityTypeChosen.projectileAbilities[0];
            projectile2 = abilityTypeChosen.projectileAbilities[1];
            abilityUIImage.GetComponent<AbilityCoolDown>().Initialize1(projectile1, abilityUIImage.GetComponent<AbilityCoolDown>().weaponHolder1);
            abilityUIImage.GetComponent<AbilityCoolDown>().Initialize2(projectile2, abilityUIImage.GetComponent<AbilityCoolDown>().weaponHolder2);
        }
        else if (abilityTypeChosen.abilityName == "ChargeAbility")
        {
            powerup1 = abilityTypeChosen.powerupAbilities[0];
            powerup2 = abilityTypeChosen.powerupAbilities[1];
            abilityUIImage.GetComponent<AbilityCoolDown>().Initialize1(powerup1, abilityUIImage.GetComponent<AbilityCoolDown>().weaponHolder1);
            abilityUIImage.GetComponent<AbilityCoolDown>().Initialize2(powerup2, abilityUIImage.GetComponent<AbilityCoolDown>().weaponHolder2);
        }
        else if (abilityTypeChosen.abilityName == "TeleportAbility")
        {
            powerup1 = abilityTypeChosen.powerupAbilities[0];
            powerup2 = abilityTypeChosen.powerupAbilities[1];
            abilityUIImage.GetComponent<AbilityCoolDown>().Initialize1(powerup1, abilityUIImage.GetComponent<AbilityCoolDown>().weaponHolder1);
            abilityUIImage.GetComponent<AbilityCoolDown>().Initialize2(powerup2, abilityUIImage.GetComponent<AbilityCoolDown>().weaponHolder2);
        }

        abilityUIImage.SetActive(true); //Sets the ability HUD game object to active so the ability can be used
    }

    #region Movement Methods
    void ForwardMovement(float forward)
    {
        if (forward > 0)
        {
            rb.AddForce(transform.forward * carInfo.carStats.speed, ForceMode.Acceleration);
        }
    }
    void BackwardMovement(float backward)
    {
        if (backward > 0)
        {
            rb.AddForce(-transform.forward * carInfo.carStats.speed / 2, ForceMode.Acceleration);
        }
    }
    void IdleMovement(float forward)
    {
        if (forward == 0)
        {
            if (carInfo.carStats.speed > 0)
            {
                carInfo.carStats.speed -= 2f;
            }
        }
    }
    void TurningMovement(float turn)
    {
        rb.AddTorque(transform.up * carInfo.carStats.turnSpeed * turn);
    }
    #endregion

    void AbilityTrigger(float abilityPower)
    {
        if (ability.canTriggerAbility && abilityUIImage.activeSelf) //Check if the ability can be fired and if the game object is active in the scene
        {
            if (abilityPower == 0)
            {
                ability.ButtonTriggered(0); //Trigger ability
            }
            else
            {
                ability.ButtonTriggered(1); //Trigger ability
            }
        }
    }
    #endregion
}