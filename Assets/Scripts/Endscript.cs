using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using XInputDotNetPure;

public class Endscript : MonoBehaviour {
	GameManager gm;

	// Use this for initialization
	void Start () {
		gm = GameManager.GetInstance ();
		PlayerIndex winner = gm.alive.ElementAt(0);

		CanvasGroup Canvas_winner = transform.Find("Player"+winner.ToString()).GetComponent<CanvasGroup>();
		Canvas_winner.alpha = 1;
	}
	GamePadState[]prevStates = new GamePadState[4];
	GamePadState[] playerStates = new GamePadState[4];

	// Update is called once per frame
	void Update () {
	
		for (int i = 0; i < playerStates.Length; i++)
		{
			prevStates[i] = playerStates[i];
			playerStates[i] = GamePad.GetState((PlayerIndex)i);
			
			if (prevStates[i].Buttons.A == ButtonState.Released && playerStates[i].Buttons.A == ButtonState.Pressed)
			{
				gm.StartRandomLevel(gm.players);
			}
			if (prevStates[i].Buttons.B == ButtonState.Released && playerStates[i].Buttons.B == ButtonState.Pressed)
			{
				Application.LoadLevel (0);
			}
		}
		
	}
}
