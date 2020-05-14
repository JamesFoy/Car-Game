using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using XInputDotNetPure;

public class CarController: MonoBehaviour
{
    #region Variable Setup
    public ControllerSetup controllerSetup;

    public Transform centerMass; //Used as the transform position for the center of mass
    public Transform rampCOM; //Used as the transform position for the center of mass

    [SerializeField, Space(10)]
    public UnityEvent triggerCamShake; //UnityEvent that is setup in editor to raise the camera shake event

    Rigidbody rb; //Reference to the rigidbody

    public GameObject speedEffect;
    public GameObject exhaustEffect;

    //TEXT VARIABLES USED FOR DEBUGING THE COMPRESSION AMOUNTS FOR EACH RAY
    //[SerializeField]
    //Text rayPoint1Text, rayPoint2Text, rayPoint3Text, rayPoint4Text;

    [SerializeField, Header("Ray Points")] Transform rayPoint1;
    [SerializeField] Transform rayPoint2;
    [SerializeField] Transform rayPoint3;
    [SerializeField] Transform rayPoint4; //References to each transform to cast each ray from

    public LayerMask layerMask;

    float dis, actualDistance; //Variables used in compression calculation

    float compressionRatio; //Varibale that contains the exact amount of compression being applied (is clamped between 0 and 1 later in script)
    float pullAmount; //Variable for pulling the car down while in the air (acts on the front of the car)

    public bool onLand; //Bool used to check if landing (currently used to play the camera shake anytime the car lands after being in the air)

    CarInfo carInfo;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        controllerSetup = GetComponent<ControllerSetup>();
        rb = GetComponent<Rigidbody>();
        carInfo = GetComponent<CarInfo>();

        //Setting a center mass removes colliders from calulation of mass(MEANING WE CAN SET COLLIDERS FREELY NOW!!!)
        rb.centerOfMass = new Vector3(centerMass.transform.localPosition.x, centerMass.transform.localPosition.y, centerMass.transform.localPosition.z); //Sets the center of mass for the car based on the local position of the COM transform.
    }

    #region Update Calls
    //Setting up the HUD speed text to a clamped speed varibale (this is based of the speed).
    private void Update()
    {
        float clampedSpeed = Mathf.Clamp(carInfo.carStats.speed, 0, carInfo.carStats.maxSpeed);
    }

    // Fixed Update is needed for all following code due to it being physics based (helps to keep everything smooth)
    void FixedUpdate()
    { 
        #region Debug Text Setup For Compression (Currently not being used)
        //DEBUG SETUP TO SEE COMPRESSION AMOUNTS IN EDITOR
        //rayPoint1Text.text = "" + compressionRatio;
        //rayPoint2Text.text = "" + compressionRatio;
        //rayPoint3Text.text = "" + compressionRatio;
        //rayPoint4Text.text = "" + compressionRatio;
        #endregion

        #region Raycast Setups
        //Debug raycasts for each ray so that you can see them in the editor (had to be .1 less the actual amount to correctly represent length or rays)
        Debug.DrawRay(rayPoint1.position, rayPoint1.TransformDirection(Vector3.forward) * 0.6f, Color.red);
        Debug.DrawRay(rayPoint2.position, rayPoint2.TransformDirection(Vector3.forward) * 0.6f, Color.red);
        Debug.DrawRay(rayPoint3.position, rayPoint3.TransformDirection(Vector3.forward) * 0.6f, Color.red);
        Debug.DrawRay(rayPoint4.position, rayPoint4.TransformDirection(Vector3.forward) * 0.6f, Color.red);

        //Debug raycasts for ground and ramp checking
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 1f, Color.red);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 7f, Color.red);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * 3f, Color.red);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * 3f, Color.red);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * 3f, Color.red);

        RaycastHit hit; //Defualt hit variable for raycasting

        // Does the ray intersect any objects (this could be changed in the futre to have layer masking however currently isnt needed)
        // Each ray is shot from a raycast point
        if (Physics.Raycast(rayPoint1.position, rayPoint1.TransformDirection(Vector3.forward), out hit, 0.6f, layerMask))
        {
            CalculateCompression(hit, rayPoint1);
        }
        if (Physics.Raycast(rayPoint2.position, rayPoint2.TransformDirection(Vector3.forward), out hit, 0.6f, layerMask))
        {
            CalculateCompression(hit, rayPoint2);
        }
        if (Physics.Raycast(rayPoint3.position, rayPoint3.TransformDirection(Vector3.forward), out hit, 0.6f, layerMask))
        {
            CalculateCompression(hit, rayPoint3);
        }
        if (Physics.Raycast(rayPoint4.position, rayPoint4.TransformDirection(Vector3.forward), out hit, 0.6f, layerMask))
        {
            CalculateCompression(hit, rayPoint4);
        }

        //Raycasts used for ground checking and ramp mechanics
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1f, layerMask))
        {
            if (hit.collider.CompareTag("Track"))
            {
                Debug.Log("On Track");
                SetRigidbodyValues(false);
                SetCOM(true);
            }
        }
        else
        {
            SetRigidbodyValues(true);
            SetCOM(false);
        }

        //Front facing ray which checks if you are falling towards something, if it is the track it will push the car up slightly (this will help to transition better from falling but also help to lift the car when coming up to a ramp)
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 7f, layerMask))
        {
            if (hit.collider.CompareTag("Track"))
            {
                rb.AddTorque(-transform.right * 50);

                SetCOM(true);
                pullAmount = 0;
            }
        }
        else
        {
            pullAmount = 40;
        }

        //Raycast for checking if the car is flipped
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, 3f, layerMask))
        {
            if (hit.collider.CompareTag("Track"))
            {
                Debug.Log("Car is upsidedown");

                //Pushes car upwards allowing for the rotation to take effect
                rb.AddForce(transform.TransformDirection(Vector3.down) * 20, ForceMode.VelocityChange);
            }
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

    //Method used for making the speed dots active when using the boost ability
    public void SpeedDots(bool isActive)
    {
        if (isActive)
        {
            speedEffect.SetActive(true);
            exhaustEffect.SetActive(false);
        }
        else
        {
            speedEffect.SetActive(false);
            exhaustEffect.SetActive(true);
        } 
    }

    #region In Air Checks & Landing Setup
    //This is used to check if the car is currently in the air (if compression = 0 it means nothing is below the car so IN AIR!!)
    public void SetRigidbodyValues(bool inAir)
    {
        if (!inAir)
        {
            //Reseting the rigidbody values for when the car lands 
            rb.mass = 1.28f;
            rb.drag = 4;
            rb.angularDrag = 10;
            carInfo.carStats.maxSpeed = 110;

           
            //Check if the player lands and if so invoke the unityevent that shakes the camera
            if (!onLand)
            {
                onLand = true;
                triggerCamShake.Invoke();
            }
        }
        else if (inAir)
        {           
            onLand = false;

            //Lowering the speed value while the car is in the air as the player shouldnt be able to effect the cars movement well in air
            carInfo.carStats.maxSpeed = 20;
            carInfo.carStats.speed = 20;

            //Setting the correct rigidbody settings for when the car is in the air, makes sure gravity has more effect etc (can be tweaked to get different resultss)
            rb.mass = 6.5f;
            rb.drag = 1;
            rb.angularDrag = 5;
        }
    }

    //Method for setting centre of mass
    public void SetCOM(bool center)
    {
        if (!center)
        {
            //Makes the car fall forward whenever it isnt on the track
            rb.AddTorque(transform.right * pullAmount);
        }
    }
    #endregion

    #region Movement Methods
    public void ForwardMovement(float forward)
    {
        if (forward > 0)
        {
            rb.AddForce(transform.forward * carInfo.carStats.speed, ForceMode.Acceleration);
        }
    }
    public void BackwardMovement(float backward)
    {
        if (backward > 0)
        {
            rb.AddForce(-transform.forward * carInfo.carStats.speed / 2, ForceMode.Acceleration);
        }
    }
    public void IdleMovement(float forward)
    {
        if (forward == 0)
        {
            if (carInfo.carStats.speed > 0)
            {
                carInfo.carStats.speed -= 2f;
            }
        }
    }
    public void TurningMovement(float turn)
    {
        rb.AddTorque(transform.up * carInfo.carStats.turnSpeed * turn);
    }
    #endregion

    #endregion
}
