using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

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
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
