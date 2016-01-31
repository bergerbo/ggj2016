using UnityEngine;
using System.Collections;
using XInputDotNetPure;
using System;
using System.Collections.Generic;
using UnityStandardAssets.Characters.ThirdPerson;

public class Player : MonoBehaviour
{

    public enum ActionName
    {
        Down, Right, Left, Up
    }

    public PlayerIndex playerIndex;
    private God god;

    #region Inputs
    private GamePadState state;
    private GamePadState prevState;
    private ThirdPersonCharacter character;
    private Animator animator;
    #endregion Inputs

    // Use this for initialization
    void Start()
    {
        god = GameObject.FindGameObjectWithTag("God").GetComponent<God>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        prevState = state;
        state = GamePad.GetState(playerIndex);

        if (state.Buttons.A == ButtonState.Pressed && prevState.Buttons.A == ButtonState.Released)
        {
            god.ProcessPlayerInput(playerIndex, ActionName.Down);
            character.TriggerAction("Down");
            return;
        }

        if (state.Buttons.B == ButtonState.Pressed && prevState.Buttons.B == ButtonState.Released)
        {
            god.ProcessPlayerInput(playerIndex, ActionName.Right);
            character.TriggerAction("Right");
            return;
        }

        if (state.Buttons.X == ButtonState.Pressed && prevState.Buttons.X == ButtonState.Released)
        {
            god.ProcessPlayerInput(playerIndex, ActionName.Left);
            character.TriggerAction("Left");
            return;
        }

        if (state.Buttons.Y == ButtonState.Pressed && prevState.Buttons.Y == ButtonState.Released)
        {
            god.ProcessPlayerInput(playerIndex, ActionName.Up);
            character.TriggerAction("Up");
            return;
        }


        if (state.Triggers.Left > 0.8 || state.Triggers.Right > 0.8)
        {
            character.Stab();
            return;
        }
    }

    public IEnumerable<ZoneOrder.Zone> GetZoneCollisions()
    {
        return GetComponentInChildren<FeetCollider>().GetZoneCollisions();
    }
}
