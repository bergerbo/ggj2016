using UnityEngine;
using System.Collections;
using System;

public class Slowed : Malus
{
    protected override void onBegin()
    {
        player.speed = player.speed * 0.7f;
    }

    protected override void onEnd()
    {
        player.speed = player.speed * (1 / 0.7f);
    }
}
