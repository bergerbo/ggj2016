using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {

	public static GameManagerScript instance;

	public static GameManagerScript GetInstance()
	{
		if(instance == null)
		{
			instance = this;
		}
		else
		{
			return instance;
		}
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
