using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public enum ActionInput
    {
        A,B,X,Y
    }

    public int playerNumber;
    private God god;

    #region Inputs
    private bool actionA = false;
    private bool prevActionA = false;

    private bool actionB = false;
    private bool prevActionB = false;

    private bool actionX = false;
    private bool prevActionX = false;

    private bool actionY = false;
    private bool prevActionY = false;
    #endregion Inputs

    // Use this for initialization
    void Start () {
        god = GameObject.FindGameObjectWithTag("God").GetComponent<God>();
    }
	
	// Update is called once per frame
	void Update () {
        prevActionA = actionA;
        prevActionB = actionB;
        prevActionX = actionX;
        prevActionY = actionY;

        actionA = Input.GetKey("joystick button 0");
        actionB = Input.GetKey("joystick button 1");
        actionX = Input.GetKey("joystick button 2");
        actionY = Input.GetKey("joystick button 3");

        if (actionA && !prevActionA)
        {
            god.ProcessPlayerInput(playerNumber, ActionInput.A);
            return;
        }

        if (actionB && !prevActionB)
        {
            god.ProcessPlayerInput(playerNumber, ActionInput.B);
            return;
        }

        if (actionX && !prevActionX)
        {
            god.ProcessPlayerInput(playerNumber, ActionInput.X);
            return;
        }

        if (actionY && ! prevActionY)
        {
            god.ProcessPlayerInput(playerNumber, ActionInput.Y);
            return;
        }
    }
}
