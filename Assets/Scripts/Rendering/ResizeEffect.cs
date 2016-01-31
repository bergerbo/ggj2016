using UnityEngine;
using System.Collections;

public class ResizeEffect : MonoBehaviour {
	private float Timer_actu = 0f;
	public float Timer_max = 1f;
	public float Speedscale = 0.1f;

	// Use this for initialization
	 void Start () {
	 	Timer_actu = Timer_max;
	 }

	// // Update is called once per frame
	void Update () {
	
		if(Timer_actu>0)
		{
			Timer_actu -= Time.deltaTime;
			transform.localScale += new Vector3(Speedscale, Speedscale, Speedscale);
		}
		else
		{
			Timer_actu = Timer_max;
			Speedscale *= -1f;
		}

	}
}
