using UnityEngine;
using System.Collections;
using System;
using UnityStandardAssets.Characters.ThirdPerson;

public class Slowed : Malus
{
    protected override void onBegin()
    {
        var character = player.GetComponentInChildren<ThirdPersonUserControl>();
        character.speed = character.speed * 0.7f;

    }

    protected override void onEnd()
    {
        var character = player.GetComponentInChildren<ThirdPersonUserControl>();
        character.speed = character.speed * (1 / 0.7f);
    }
}
