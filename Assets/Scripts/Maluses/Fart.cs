using UnityEngine;
using System.Collections;
using System;

public class Fart : Malus
{
    protected override void onBegin()
    {
        Debug.Log("Player " + player.playerNumber + " farted");
    }

    protected override void onEnd(){}
}
