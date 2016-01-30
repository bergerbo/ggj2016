using UnityEngine;
using System.Collections;
using System;
using UnityStandardAssets.Characters.ThirdPerson;

public class Confused : Malus
{
    protected override void onBegin()
    {
        player.GetComponent<ThirdPersonUserControl>().inversion = -1;
    }

    protected override void onEnd()
    {
        player.GetComponent<ThirdPersonUserControl>().inversion = 1;
    }
}
