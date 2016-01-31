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

		if(Input.GetKeyUp(KeyCode.JoystickButton0))
		{
			playerDetection.SetActive(true);
			GameManager.GetInstance().gameState = GameManager.GameState.PLAYERDETECTION;
		}
		else if(Input.GetKeyUp(KeyCode.JoystickButton3))
		{
			credits.SetActive(true);
			GameManager.GetInstance().gameState = GameManager.GameState.CREDITS;
		}
		else if(Input.GetKey(KeyCode.JoystickButton2))
		{
			Application.Quit();
		}

		if(GameManager.GetInstance().gameState == GameManager.GameState.CREDITS)
		{
			if(Input.anyKey)
				credits.SetActive(false);
		}
	}
}
