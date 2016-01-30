using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using XInputDotNetPure;
using Random = UnityEngine.Random;

public class PlayerDetection : MonoBehaviour {
	// private PlayerIndex playerIndex;
	// private bool playerIndexSet;

	public GameObject[] playerIcons;

	public GamePadState[] playerStates;
	public GamePadState[] prevStates;

	public GameObject readyImage;
	// GamePadState state;
 //    GamePadState prevState;


	private GameManager gameManager;
	// Use this for initialization
	void Start () {
		playerStates = new GamePadState[4];
		prevStates = new GamePadState[4];
		gameManager = GameManager.GetInstance();
		// GamePad.GetState(playerIndex);
		// if (!playerIndexSet || !prevState.IsConnected)
  //       {
            
	}
	
	// Update is called once per frame
	void Update () {
		if (gameManager.gameState == GameManager.GameState.PLAYERSELECTION)
		{
			for (int i = 0; i < 4; ++i)
	            {
	                PlayerIndex testPlayerIndex = (PlayerIndex)i;
	                GamePadState testState = GamePad.GetState(testPlayerIndex);
	                if (testState.IsConnected)
	                {
	                	prevStates[i] = playerStates[i];
	                	playerStates[i] = testState;
	                    // Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
	                    // Debug.Log(playerStates[i]);
	                    // playerIndex = testPlayerIndex;
	                    // playerIndexSet = true;
	                }
	        }
			// state = GamePad.GetState(playerIndex);

	        // Detect if a button was pressed this frame

	        for(int i =0; i < playerStates.Length; i++)
	        {
		        if (prevStates[i].Buttons.A == ButtonState.Released && playerStates[i].Buttons.A == ButtonState.Pressed)
		        {
		        	if(!gameManager.playersReady.ContainsKey((PlayerIndex)i)){

		            	gameManager.playersReady.Add((PlayerIndex)i, true);
		            	playerIcons[i].SetActive(true);
		            	// Debug.Log("Player"+i+" ready");

		            	// Debug.Log(gameManager.playersReady.Count);
		        	}

		        }
	        }
		}
	}
}
