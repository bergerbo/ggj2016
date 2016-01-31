using UnityEngine;
using System.Collections;

public class ScrollMainMenu : MonoBehaviour {

	public GameObject playerDetection;
	public GameObject credits;
	// Use this for initialization
	void Start () {
		// playerDetection = GameObject.Find("PlayerDetection");
	}
	
	// Update is called once per frame
	void Update () {

		if(GameManager.GetInstance().gameState == GameManager.GameState.MAINMENU)
		{
			if(Input.GetKeyUp(KeyCode.JoystickButton0)) // = A
			{
				playerDetection.SetActive(true);
				GameManager.GetInstance().gameState = GameManager.GameState.PLAYERDETECTION;
			}

			else if(Input.GetKeyUp(KeyCode.JoystickButton3)) // = Y
			{
				credits.SetActive(true);
				GameManager.GetInstance().gameState = GameManager.GameState.CREDITS;
			}

			else if(Input.GetKeyUp(KeyCode.JoystickButton1)) // = B
			{
				Application.Quit();
			}
		}

		if(GameManager.GetInstance().gameState == GameManager.GameState.CREDITS)
		{
			if(Input.GetKeyUp(KeyCode.JoystickButton1)) // = B
			{
				credits.SetActive(false);
				GameManager.GetInstance().gameState = GameManager.GameState.MAINMENU;
			}
		}
	}
}
