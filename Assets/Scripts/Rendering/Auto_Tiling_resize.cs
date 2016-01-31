using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Auto_Tiling_resize : MonoBehaviour {
	private Renderer rend;
	//public Shader shader1;
    // Use this for initialization
	void Start()
    {
		rend = GetComponent<Renderer>();

		float ScaleX_f = transform.localScale.x;
		float ScaleY_f = transform.localScale.z;

		rend.material.mainTextureScale= new Vector2(ScaleX_f/3,ScaleY_f/3);

		//rend.material.SetFloat("_TilingX", ScaleX_f/3);
		//rend.material.SetFloat("_TilingY", ScaleY_f/3);
	
    }

}
