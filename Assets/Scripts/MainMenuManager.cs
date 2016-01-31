using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using XInputDotNetPure;
using Random = UnityEngine.Random;

public class MainMenuManager : MonoBehaviour
{

    public GameObject[] playerIcons;

    public GamePadState[] playerStates;
    public GamePadState[] prevStates;

    // public GameObject readyImage;

    private GameManager gameManager;

    public List<PlayerIndex> playersReady = new List<PlayerIndex>();
    public GameObject howToPlay;
    public GameObject startButton;


    // Use this for initialization
    void Start()
    {
        playerStates = new GamePadState[4];
        prevStates = new GamePadState[4];
        gameManager = GameManager.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameState == GameManager.GameState.PLAYERDETECTION)
        {
            // Detect if a button was pressed this frame

            for (int i = 0; i < playerStates.Length; i++)
            {
                prevStates[i] = playerStates[i];
                playerStates[i] = GamePad.GetState((PlayerIndex)i);

                if (prevStates[i].Buttons.A == ButtonState.Released && playerStates[i].Buttons.A == ButtonState.Pressed)
                {
                	
                    if (!playersReady.Contains((PlayerIndex)i))
                    {
                    	if(playersReady.Count>1)
                    	{
                    		startButton.SetActive(true);
                    	}
                        playersReady.Add((PlayerIndex)i);
                        playerIcons[i].SetActive(true);
                    }
                }
                if (prevStates[i].Buttons.Start == ButtonState.Released && playerStates[i].Buttons.Start == ButtonState.Pressed)
                {
                	howToPlay.SetActive(true);
                }
            }
        }

        if(gameManager.gameState == GameManager.GameState.HOWTO)
        {
            for (int i = 0; i < playerStates.Length; i++)
            {
                prevStates[i] = playerStates[i];
                playerStates[i] = GamePad.GetState((PlayerIndex)i);

                if (prevStates[i].Buttons.Start == ButtonState.Released && playerStates[i].Buttons.Start == ButtonState.Pressed)
                {
                    howToWin.SetActive(true); 
                    StartRandomLevel();
                }
        }
    }

    public void StartRandomLevel()
    {
        gameManager.StartRandomLevel(playersReady.ToArray());
    }

}
