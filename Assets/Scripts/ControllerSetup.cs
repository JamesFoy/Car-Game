using UnityEngine;
using XInputDotNetPure; // Required in C#

public class ControllerSetup : MonoBehaviour
{
    CarController carController;

    public bool playerIndexSet = false;
    public PlayerIndex player1Index;
    public PlayerIndex player2Index;
    public GamePadState state1;
    public GamePadState state2;
    public GamePadState prevState1;
    public GamePadState prevState2;

    // Use this for initialization
    void Start()
    {
        carController = GetComponent<CarController>();
    }

    void FixedUpdate()
    {
        // SetVibration should be sent in a slower rate.
        // Set vibration according to triggers
        GamePad.SetVibration(player1Index, state1.Triggers.Left, state1.Triggers.Right);
        GamePad.SetVibration(player2Index, state2.Triggers.Left, state2.Triggers.Right);
    }

    // Update is called once per frame
    void Update()
    {
        // Find a PlayerIndex, for a single player game
        // Will find the first controller that is connected ans use it
        if (!playerIndexSet || !prevState1.IsConnected || !prevState2.IsConnected)
        {
            if (carController.thisNumber == CarController.PlayerNumber.p1)
            {
                PlayerIndex players1Index = PlayerIndex.One;
                GamePadState state1 = GamePad.GetState(players1Index);

                if (state1.IsConnected)
                {
                    Debug.Log(string.Format("GamePad found {0}", players1Index));
                    player1Index = players1Index;
                    playerIndexSet = true;
                }
            }
            else if (carController.thisNumber == CarController.PlayerNumber.p2)
            {
                PlayerIndex players2Index = PlayerIndex.Two;
                GamePadState state2 = GamePad.GetState(players2Index);

                if (state2.IsConnected)
                {
                    Debug.Log(string.Format("GamePad found {0}", players2Index));
                    player2Index = players2Index;
                    playerIndexSet = true;
                }
            }
        }

        prevState1 = state1;
        state1 = GamePad.GetState(player1Index);
        prevState2 = state2;
        state2 = GamePad.GetState(player2Index);

        //if (state1.Buttons.A == ButtonState.Pressed)
        //{
        //    Debug.Log("Controller 1 Press A");
        //}

        //if (state2.Buttons.A == ButtonState.Pressed)
        //{
        //    Debug.Log("Controller 2 Press A");
        //}
    }
}
