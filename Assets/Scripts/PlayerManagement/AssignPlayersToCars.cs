using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignPlayersToCars : MonoBehaviour
{
    public int numberOfHumanPlayers = 1;

    private void Start()
    {
        AssignPlayers(numberOfHumanPlayers);
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
