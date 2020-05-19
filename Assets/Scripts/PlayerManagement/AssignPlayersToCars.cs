using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class AssignPlayersToCars : MonoBehaviour
{
    public AssignPlayersToCars Instance;
    public int numberOfHumanPlayers = 1;
    public List<GameObject> ListOfHumanAssignedCars { get; private set; } = new List<GameObject>();

    [SerializeField] GameObject realCamPrefab;
    [SerializeField] GameObject virtualCamPrefab;

    [SerializeField] GameObject UICanvas;
    [SerializeField] GameObject playerUIPrefab;

    [SerializeField] List<AbilitySet> player1AbilitySets;
    [SerializeField] List<AbilitySet> player2AbilitySets;

    [SerializeField] GameEvent pickupDetectedPlayer1;
    [SerializeField] GameEvent pickupDetectedPlayer2;
    [SerializeField] GameEvent pickupDetectedAI;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        AssignPlayers(numberOfHumanPlayers);
        AssignGameObjectsToPlayers(numberOfHumanPlayers);
    }
    public void AssignPlayers(int numberOfHumanPlayers)
    {
        List<GameObject> listOfCars = new List<GameObject>();
        listOfCars.AddRange(GameObject.FindGameObjectsWithTag("Player"));

        for (int i = 0; i < listOfCars.Count; i++)
        {
            Debug.Log(listOfCars[i].name);
        }

        for (int i = 0; i < numberOfHumanPlayers; i++)
        {
            CarInfo carInfo = ValidateThisCar(listOfCars[i]);

            HumanPlayer humanPlayerScriptableObject = ScriptableObject.CreateInstance<HumanPlayer>();
            humanPlayerScriptableObject.playerNumber = (i + 1);
            carInfo.carStats.playerType = humanPlayerScriptableObject;

            carInfo.GetComponent<InputManager>().enabled = true;
            carInfo.GetComponent<ControllerSetup>().enabled = true;

            listOfCars[i].name = "Player " + (i + 1);
            ListOfHumanAssignedCars.Add(listOfCars[i]);

            Debug.Log(listOfCars[i].name + " assigned");
        }

        listOfCars.RemoveRange(0, numberOfHumanPlayers);

        for (int i = 0; i < listOfCars.Count; i++)
        {
            CarInfo carInfo = ValidateThisCar(listOfCars[i]);

            carInfo.carStats.playerType = ScriptableObject.CreateInstance<ComputerPlayer>();

            carInfo.GetComponent<AIPathControl>().enabled = true;

            listOfCars[i].name = "AI " + (i + 1);

            listOfCars[i].GetComponent<GameEventListener>().OnEventChanged(pickupDetectedAI);

            Debug.Log("AI assigned to: " + listOfCars[i].name);
        }
    }
    private void AssignGameObjectsToPlayers(int numberOfHumanPlayers)
    {
        List<GameObject> realCamList = new List<GameObject>();
        List<GameObject> virtualCamList = new List<GameObject>();
        List<RectTransform> playerUIList = new List<RectTransform>();

        for (int i = 1; i <= ListOfHumanAssignedCars.Count; i++)
        {
            GameObject realCam = Instantiate(realCamPrefab);
            GameObject virtualCam = Instantiate(virtualCamPrefab);
            GameObject playerUI = Instantiate(playerUIPrefab);

            realCam.GetComponent<Camera>().cullingMask |= 1 << (20 + i);
            realCamList.Add(realCam);

            virtualCam.layer = (20 + i);
            virtualCam.GetComponent<CinemachineVirtualCamera>().LookAt = ListOfHumanAssignedCars[i - 1].transform;
            virtualCam.GetComponent<CinemachineVirtualCamera>().Follow = ListOfHumanAssignedCars[i - 1].transform;
            virtualCamList.Add(virtualCam);

            playerUI.transform.SetParent(UICanvas.transform, false);
            playerUI.GetComponentInChildren<SpeedSliderManagement>().carInfo = ListOfHumanAssignedCars[i - 1].GetComponent<CarInfo>();
            playerUI.GetComponentInChildren<DamageDisplayManagement>().carInfo = ListOfHumanAssignedCars[i - 1].GetComponent<CarInfo>();
            playerUI.name = "Player " + i + " UI";
            playerUIList.Add(playerUI.GetComponent<RectTransform>());

            ListOfHumanAssignedCars[i - 1].GetComponent<AbilityInitializer>().LinkAbilityUIToACar(playerUI.GetComponentInChildren<AbilityUI>(true));
        }
        // methods to assign cameras relative to UI
        if (numberOfHumanPlayers >= 1)
        {
            realCamList[0].GetComponent<Camera>().rect = new Rect(0, 0, 1, 1);
            ListOfHumanAssignedCars[0].GetComponent<AbilityInitializer>().DefineAbilitySets(player1AbilitySets);
            ListOfHumanAssignedCars[0].GetComponent<GameEventListener>().OnEventChanged(pickupDetectedPlayer1);
        }
        if (numberOfHumanPlayers >= 2)
        {
            realCamList[0].GetComponent<Camera>().rect = new Rect(0, 0.5f, 0.7f, 0.5f);
            realCamList[1].GetComponent<Camera>().rect = new Rect(0.3f, 0, 0.7f, 0.5f);
            playerUIList[0].offsetMax = new Vector2(-1152, 0);
            playerUIList[0].offsetMin = new Vector2(0, 1080);
            playerUIList[1].offsetMax = new Vector2(0, -1080);
            playerUIList[1].offsetMin = new Vector2(1152, 0);
            ListOfHumanAssignedCars[1].GetComponent<AbilityInitializer>().DefineAbilitySets(player2AbilitySets);
            ListOfHumanAssignedCars[1].GetComponent<GameEventListener>().OnEventChanged(pickupDetectedPlayer2);
        }
        if (numberOfHumanPlayers >= 3)
        {
            realCamList[0].GetComponent<Camera>().rect = new Rect(0, 0.5f, 0.5f, 0.5f);
            realCamList[1].GetComponent<Camera>().rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
            realCamList[2].GetComponent<Camera>().rect = new Rect(0, 0, 0.5f, 0.5f);
        }
        if (numberOfHumanPlayers >= 4)
        {
            realCamList[3].GetComponent<Camera>().rect = new Rect(0.5f, 0, 0.5f, 0.5f);
        }
        else
        {

        }
    }
    private CarInfo ValidateThisCar(GameObject car)
    {
        CarInfo carInfo = car.GetComponent<CarInfo>();
        CheckForExistingCarStats(carInfo);
        return carInfo;
    }
    private void CheckForExistingCarStats(CarInfo carInfo)
    {
        if (carInfo.carStats == null)
        {
            carInfo.CheckForValidCarStats();
        }
    }
}
