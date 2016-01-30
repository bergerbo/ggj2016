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

    private List<ZoneOrder.Zone> collidingZones;

    #region Inputs
    private GamePadState state;
    private GamePadState prevState;
    #endregion Inputs

    // Use this for initialization
    void Start()
    {
        god = GameObject.FindGameObjectWithTag("God").GetComponent<God>();
        collidingZones = new List<ZoneOrder.Zone>();
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
        return collidingZones;
    }

    public void OnTriggerEnter(Collider collider)
    {

        switch (collider.transform.tag)
        {
            case "Bois":
                collidingZones.Add(ZoneOrder.Zone.Bois);
                break;
            case "Mur":
                collidingZones.Add(ZoneOrder.Zone.Mur);
                break;
            case "Pierre":
                collidingZones.Add(ZoneOrder.Zone.Pierre);
                break;
            case "Tapis":
                collidingZones.Add(ZoneOrder.Zone.Tapis);
                break;
            case "Herbe":
                collidingZones.Add(ZoneOrder.Zone.Herbe);
                break;
            case "Terre":
                collidingZones.Add(ZoneOrder.Zone.Terre);
                break;
        }
    }

    public void OnTriggerExit(Collider collider)
    {
        switch (collider.transform.tag)
        {
            case "Bois":
                collidingZones.Remove(ZoneOrder.Zone.Bois);
                break;
            case "Mur":
                collidingZones.Remove(ZoneOrder.Zone.Mur);
                break;
            case "Pierre":
                collidingZones.Remove(ZoneOrder.Zone.Pierre);
                break;
            case "Tapis":
                collidingZones.Remove(ZoneOrder.Zone.Tapis);
                break;
            case "Herbe":
                collidingZones.Remove(ZoneOrder.Zone.Herbe);
                break;
            case "Terre":
                collidingZones.Remove(ZoneOrder.Zone.Terre);
                break;
        }
    }
}
