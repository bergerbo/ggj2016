using UnityEngine;
using System.Collections;

public class ColorSwap : MonoBehaviour {

	private Material material;
	// Use this for initialization
	void Start () {
		material = GetComponent<Renderer>().material;
		var r = UnityEngine.Random.Range(0, 220f)/100;
		var g = UnityEngine.Random.Range(0, 220f)/100;
		var b = UnityEngine.Random.Range(0, 220f)/100;
		
		int v = Mathf.RoundToInt(UnityEngine.Random.Range(1, 3));

		if(v == 3){
			r= 0;
		}else if(v == 2){
			g=0;
		}else if(v==0){
			b=0;
		}
			
		
		// habitColor = material.GetTag("Habit Color", true, "Nothing");
		// habitColor.color = new Vector3(r, g, b);
		// material.HabitColor = new Color(r, g, b);
		material.SetColor("_HabitColor", new Color(r,g,b));
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
