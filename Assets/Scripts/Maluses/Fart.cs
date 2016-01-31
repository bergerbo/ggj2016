using UnityEngine;
using System.Collections;
using System;

public class Fart : Malus
{
    public GameObject fart;

    protected override void onBegin()
    {
        Debug.Log("Player " + player.playerIndex + " farted");
		GameObject.Find ("Audio").GetComponent<Soundscript>().Play_sound ("Fart");

        var controller = player.GetComponentInChildren<ThirdPersonCharacter>();
        var instance = (GameObject)Instantiate(fart,controller.transform.position,Quaternion.identity);
        instance.transform.localScale = instance.transform.localScale * (2 + 2 *UnityEngine.Random.value);
        Destroy(instance, duration);
    }

    protected override void onEnd()
    {

    }
}
