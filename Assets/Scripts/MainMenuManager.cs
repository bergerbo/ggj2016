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
	public ScrollMainMenu scrollMainMenu;

    // public GameObject readyImage;

    private GameManager gameManager;

    public List<PlayerIndex> playersReady = new List<PlayerIndex>();
    public GameObject howToWin;
    public GameObject startButton;

	public bool GoStart = false;
	
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
							playersReady.Add((PlayerIndex)i);
							playerIcons[i].SetActive(true);
							
							if(playersReady.Count>=2)
							{
								startButton.SetActive(true);
								GoStart = true;
							}
		                        
						}
					else 
						{
							if(playersReady.Count<=1)
							{
								startButton.SetActive(false);
								GoStart = false;
							}
							playersReady.Remove((PlayerIndex)i);
							playerIcons[i].SetActive(false);
						}
					}
                

				if (prevStates[i].Buttons.Start == ButtonState.Released && playerStates[i].Buttons.Start == ButtonState.Pressed && GoStart)
				{
					gameManager.gameState = GameManager.GameState.HOWTO;
					howToWin.SetActive(true);
					// howToPlay.SetActive(true);
				}
            }

			if(Input.GetKeyUp(KeyCode.JoystickButton1)) // = B
			{
				if(playersReady.Count<=0)
				{
					scrollMainMenu.playerDetection.SetActive(false);
					GameManager.GetInstance().gameState = GameManager.GameState.MAINMENU;
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
                    StartRandomLevel();
                }
            }

			if(Input.GetKeyUp(KeyCode.JoystickButton1)) // = B
			{
				howToWin.SetActive(false);
				GameManager.GetInstance().gameState = GameManager.GameState.PLAYERDETECTION;
			}
        }
    }

    public void StartRandomLevel()
    {
        gameManager.StartRandomLevel(playersReady.ToArray());
    }

}
