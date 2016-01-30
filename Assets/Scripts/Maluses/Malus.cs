using UnityEngine;
using System.Collections;

public abstract class Malus : MonoBehaviour {

    protected Player player;

    public float duration;
    private float timeSinceBegin;

    protected abstract void onBegin();
    protected abstract void onEnd();

	// Use this for initialization
	void Start () {
        player = GetComponent<Player>();
        onBegin();
	}
	
    // Update is called once per frame
	void Update () {
        timeSinceBegin += Time.deltaTime;

        if(timeSinceBegin>duration)
        {
            Destroy();
        }
	}

    public static void Test()
    {

    }

    public void Destroy()
    {
        onEnd();
        Destroy(this);
    }
}
