﻿using UnityEngine;
using System.Collections;
using System;

public class Confused : Malus
{
    protected override void onBegin()
    {
        player.inversion = -1;
    }

    protected override void onEnd()
    {
        player.inversion = 1;
    }
}
