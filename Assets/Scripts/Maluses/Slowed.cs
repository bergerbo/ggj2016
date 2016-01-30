using UnityEngine;
using System.Collections;
using System;
using UnityStandardAssets.Characters.ThirdPerson;

public class Slowed : Malus
{
    protected override void onBegin()
    {
        player.GetComponentInChildren<ThirdPersonCharacter>().m_MoveSpeedMultiplier = 0.7f;

    }

    protected override void onEnd()
    {
        player.GetComponentInChildren<ThirdPersonCharacter>().m_MoveSpeedMultiplier = 1f;
    }
}
