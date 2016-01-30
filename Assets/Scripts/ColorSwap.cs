using UnityEngine;
using System.Collections;

public class ColorSwap : MonoBehaviour {

	private Material material;
	// Use this for initialization
	void Start () {
		material = GetComponent<Renderer>().material;
		var r = UnityEngine.Random.Range(50, 255f)/100;
		var g = UnityEngine.Random.Range(50, 255f)/100;
		var b = UnityEngine.Random.Range(50, 255f)/100;

		// habitColor = material.GetTag("Habit Color", true, "Nothing");
		// habitColor.color = new Vector3(r, g, b);
		// material.HabitColor = new Color(r, g, b);
		material.SetColor("_HabitColor", new Color(r,g,b));
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
