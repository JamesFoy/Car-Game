using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AssignPlayersToCars : MonoBehaviour
{
    public static AssignPlayersToCars Instance;
    public int numberOfHumanPlayers = 1;
    public static List<GameObject> ListOfHumanAssignedCars { get; private set; } = new List<GameObject>();

    [SerializeField] GameObject realCamPrefab;
    [SerializeField] GameObject virtualCamPrefab;

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
        AssignCamerasToPlayers(numberOfHumanPlayers);
    }
    public static void AssignPlayers(int numberOfHumanPlayers)
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

            Debug.Log("AI assigned to: " + listOfCars[i].name);
        }
    }
    private void AssignCamerasToPlayers(int numberOfHumanPlayers)
    {
        for (int i = 1; i <= ListOfHumanAssignedCars.Count; i++)
        {
            GameObject realCam = Instantiate(realCamPrefab);
            GameObject virtualCam = Instantiate(virtualCamPrefab);
            realCam.GetComponent<Camera>().cullingMask |= 1 << (20 + i);
            virtualCam.GetComponent<CinemachineVirtualCamera>().LookAt = ListOfHumanAssignedCars[i-1].transform;
            virtualCam.GetComponent<CinemachineVirtualCamera>().Follow = ListOfHumanAssignedCars[i-1].transform;
        }
    }
    private static CarInfo ValidateThisCar(GameObject car)
    {
        CarInfo carInfo = car.GetComponent<CarInfo>();
        CheckForExistingCarStats(carInfo);
        return carInfo;
    }
    private static void CheckForExistingCarStats(CarInfo carInfo)
    {
        if (carInfo.carStats == null)
        {
            carInfo.CheckForValidCarStats();
        }
    }
}
