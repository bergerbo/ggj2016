using UnityEngine;
using System.Collections;
using XInputDotNetPure;
using System;
using System.Collections.Generic;

public class Player : MonoBehaviour
{

    public enum ActionInput
    {
        A, B, X, Y
    }

    public PlayerIndex playerIndex;
    private God god;

    #region Inputs
    private GamePadState state;
    private GamePadState prevState;
    #endregion Inputs

    // Use this for initialization
    void Start()
    {
        god = GameObject.FindGameObjectWithTag("God").GetComponent<God>();
    }

    // Update is called once per frame
    void Update()
    {
        prevState = state;
        state = GamePad.GetState(playerIndex);

        if (state.Buttons.A == ButtonState.Pressed && prevState.Buttons.A == ButtonState.Released)
        {
            god.ProcessPlayerInput(playerIndex, ActionInput.A);
            return;
        }

        if (state.Buttons.B == ButtonState.Pressed && prevState.Buttons.B == ButtonState.Released)
        {
            god.ProcessPlayerInput(playerIndex, ActionInput.B);
            return;
        }

        if (state.Buttons.X == ButtonState.Pressed && prevState.Buttons.X == ButtonState.Released)
        {
            god.ProcessPlayerInput(playerIndex, ActionInput.X);
            return;
        }

        if (state.Buttons.Y == ButtonState.Pressed && prevState.Buttons.Y == ButtonState.Released)
        {
            god.ProcessPlayerInput(playerIndex, ActionInput.Y);
            return;
        }
    }

    public IEnumerable<ZoneOrder.Zone> GetZoneCollisions()
    {
        return GetComponentInChildren<FeetCollider>().GetZoneCollisions();
    }
}
