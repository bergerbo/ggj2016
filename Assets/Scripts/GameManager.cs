using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;
using System.Linq;

public class GameManager : MonoBehaviour {

	public enum GameState
	{
		MAINMENU,
		PLAYERDETECTION,
		TUTO,
		CREDITS,
		HOWTO,
		RUN,
		PAUSE,
		VICTORY
	}

	public GameState gameState;

    public PlayerIndex[] players;

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

	void Awake(){
		if(instance == null)
		{
			instance = this;
		}
		DontDestroyOnLoad(gameObject);
	}

    public void StartRandomLevel(PlayerIndex[] players)
    {
        this.players = players;
        gameState = GameState.PLAYERDETECTION;
        var rng = new System.Random();
        Application.LoadLevel("LD0"+rng.Next(1, 6));
    }
}
