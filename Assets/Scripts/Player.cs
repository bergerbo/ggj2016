using UnityEngine;
using System.Collections;
using XInputDotNetPure;
using System;
using System.Collections.Generic;

public class Player : MonoBehaviour
{

    public enum ActionName
    {
        Down, Right, Left, Up
    }

    public PlayerIndex playerIndex;
    public God god;

    #region Inputs
    private GamePadState state;
    private GamePadState prevState;
    private ThirdPersonCharacter character;
    private Animator animator;
    private Transform m_Cam;
    public float inversion;
    public float speed;
    #endregion Inputs

    // Use this for initialization
    void Start()
    {
        god = GameObject.FindGameObjectWithTag("God").GetComponent<God>();
        animator = GetComponentInChildren<Animator>();
        character = GetComponentInChildren<ThirdPersonCharacter>();

        m_Cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        prevState = state;
        state = GamePad.GetState(playerIndex);
        
        if (state.Buttons.A == ButtonState.Pressed && prevState.Buttons.A == ButtonState.Released)
        {
            if (character.TriggerAction("Down"))
                god.ProcessPlayerInput(playerIndex, ActionName.Down);
            return;
        }

        if (state.Buttons.B == ButtonState.Pressed && prevState.Buttons.B == ButtonState.Released)
        {
            if (character.TriggerAction("Right"))
                god.ProcessPlayerInput(playerIndex, ActionName.Right);
            return;
        }

        if (state.Buttons.X == ButtonState.Pressed && prevState.Buttons.X == ButtonState.Released)
        {
            if (character.TriggerAction("Left"))
                god.ProcessPlayerInput(playerIndex, ActionName.Left);
            return;
        }

        if (state.Buttons.Y == ButtonState.Pressed && prevState.Buttons.Y == ButtonState.Released)
        {
            if (character.TriggerAction("Up"))
                god.ProcessPlayerInput(playerIndex, ActionName.Up);
            return;
        }


        if (state.Triggers.Left > 0.8 || state.Triggers.Right > 0.8)
        {
            character.Stab();
            return;
        }

        if (!character.isInAnimation())
        {
            // read inputs
            float h = state.ThumbSticks.Left.X;
            float v = state.ThumbSticks.Left.Y;

            // calculate camera relative direction to move:
            var m_CamForward = Vector3.Scale(m_Cam.up, new Vector3(1, 0, 1)).normalized;
            var m_Move = speed * inversion * v * m_CamForward + speed * inversion * h * m_Cam.right;

            // pass all parameters to the character control script
            character.Move(m_Move, true);

            character.GetComponent<Rigidbody>().velocity = m_Move;
        }
    }

    public IEnumerable<ZoneOrder.Zone> GetZoneCollisions()
    {
        return GetComponentInChildren<FeetCollider>().GetZoneCollisions();
    }
}
