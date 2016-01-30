using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;

public class GameManager : MonoBehaviour {

	public enum GameState
	{
		MAINMENU,
		PLAYERSELECTION,
		TUTO,
		CREDITS,
		RUN,
		PAUSE,
		VICTORY
	}

	public GameState gameState;

	public Dictionary<PlayerIndex, bool> playersReady = new Dictionary<PlayerIndex, bool>();

	public static GameManager instance;

	public static GameManager GetInstance()
	{
		if(instance == null)
		{
			instance = new GameManager();
		}
		
		return instance;
	}

	public delegate void OnPause();
	public event OnPause onPause;

	public GameObject mainMenuUI;
	public GameObject playerDetectionUI;

	void Awake(){
		if(instance == null)
		{
			instance = this;
		}
		DontDestroyOnLoad(gameObject);
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LaunchPlayerDetection()
	{
		mainMenuUI.SetActive(false);
		playerDetectionUI.SetActive(true);
		gameState = GameState.PLAYERSELECTION;
	}
}
