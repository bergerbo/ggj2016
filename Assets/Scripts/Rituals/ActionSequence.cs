﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using XInputDotNetPure;
using System.Linq;

public class ActionSequence : Ritual
{
    public Action[] actions;
    private Dictionary<PlayerIndex,int> playersStates;

    public ActionSequence(PlayerIndex[] players, Action[] actions)
    {
        this.actions = actions;
        this.playersStates = new Dictionary<PlayerIndex, int>();
        foreach(var player in players)
        {
            playersStates.Add(player, 0);
        }
    }

    public bool ProcessInput(PlayerIndex playerNumber, Player.ActionInput input)
    {
        var playerState = playersStates[playerNumber];
        if (actions[playerState].input == input)
        {
            playersStates[playerNumber] = playerState + 1;
        }
        return false;
    }

    public IEnumerable<PlayerIndex> UnfaithfulPlayers()
    {

        foreach(var playerState in playersStates)
        {
            if (playerState.Value < actions.Length)
                yield return playerState.Key;
        }
    }

    public void Explain()
    {
        var str = "For ritual next press buttons:";
        foreach (Action action in actions)
        {
            str += " " + Enum.GetName(action.input.GetType(), action.input);
        }
        Debug.Log(str);

    }
}
