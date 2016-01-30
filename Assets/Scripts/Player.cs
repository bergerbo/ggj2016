using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public enum ActionInput
    {
        A,B,X,Y
    }

    public int playerNumber;
    private God god;

	// Use this for initialization
	void Start () {
        god = GameObject.FindGameObjectWithTag("God").GetComponent<God>();
	}
	
	// Update is called once per frame
	void Update () {
        bool actionA = Input.GetKey("joystick " + playerNumber + " button 0");
        if (actionA)
        {
            god.ProcessPlayerInput(playerNumber, ActionInput.A);
            return;
        }

        bool actionB = Input.GetKeyDown("joystick " + playerNumber + " button 1");
        if (actionB)
        {
            god.ProcessPlayerInput(playerNumber, ActionInput.B);
            return;
        }

        bool actionX = Input.GetKeyDown("joystick " + playerNumber + " button 2");
        if (actionX)
        {
            god.ProcessPlayerInput(playerNumber, ActionInput.X);
            return;
        }

        bool actionY = Input.GetKeyDown("joystick " + playerNumber + " button 3");
        if (actionY)
        {
            god.ProcessPlayerInput(playerNumber, ActionInput.Y);
            return;
        }
    }
}
