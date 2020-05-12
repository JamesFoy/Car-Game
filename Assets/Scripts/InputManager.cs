using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using XInputDotNetPure;

public class InputManager : MonoBehaviour
{
    public enum PlayerNumber { p1, p2 }; //Enum setup for setting players, currently only p1 works
    public PlayerNumber thisNumber;

    ControllerSetup controllerSetup;
    CarController carController;
    CarInfo carInfo;

    // Start is called before the first frame update
    void Start()
    {
        controllerSetup = GetComponent<ControllerSetup>();
        carController = GetComponent<CarController>();
        carInfo = GetComponent<CarInfo>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float forward = 0;
        float backward = 0;
        float turn = 0;

        #region Player 1 Controls
        if (thisNumber == PlayerNumber.p1)
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
                    GetComponent<AbilityInitializer>().TriggerAbilitySequence(AbilityDeployModes.DeployStyle.Attack);
                }
                if (controllerSetup.state1.Buttons.LeftShoulder == ButtonState.Pressed)
                {
                    GetComponent<AbilityInitializer>().TriggerAbilitySequence(AbilityDeployModes.DeployStyle.Defense);
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
                if (Input.GetKey(KeyCode.E))
                {
                    GetComponent<AbilityInitializer>().TriggerAbilitySequence(AbilityDeployModes.DeployStyle.Attack);
                }
                if (Input.GetKey(KeyCode.Q))
                {
                    GetComponent<AbilityInitializer>().TriggerAbilitySequence(AbilityDeployModes.DeployStyle.Defense);
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
                    GetComponent<AbilityInitializer>().TriggerAbilitySequence(AbilityDeployModes.DeployStyle.Attack);
                }
                if (controllerSetup.state2.Buttons.LeftShoulder == ButtonState.Pressed)
                {
                    GetComponent<AbilityInitializer>().TriggerAbilitySequence(AbilityDeployModes.DeployStyle.Defense);
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
                    GetComponent<AbilityInitializer>().TriggerAbilitySequence(AbilityDeployModes.DeployStyle.Attack);
                }
                if (Input.GetKey(KeyCode.KeypadPeriod))
                {
                    GetComponent<AbilityInitializer>().TriggerAbilitySequence(AbilityDeployModes.DeployStyle.Defense);
                }
            }
            #endregion
        }
        #endregion

        carInfo.carStats.speed = Mathf.Lerp(carInfo.carStats.speed, carInfo.carStats.maxSpeed, forward * Time.deltaTime / 1f);
        carInfo.carStats.speed = Mathf.Lerp(carInfo.carStats.speed, carInfo.carStats.maxSpeed, backward * Time.deltaTime / 1f);
        ApplyInputMovement(forward, backward, turn);
    }

    void ApplyInputMovement(float forwardInput, float backwardInput, float turnInput)
    {
        carController.ForwardMovement(forwardInput);
        carController.BackwardMovement(backwardInput);
        carController.IdleMovement(forwardInput);
        carController.TurningMovement(turnInput);
    }
}
