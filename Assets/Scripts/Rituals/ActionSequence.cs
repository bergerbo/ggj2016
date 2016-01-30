using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using XInputDotNetPure;

public class ActionSequence : Ritual
{
    private Action[] actions;
    private int[] playersStates;

    public ActionSequence(int playersNumber, Action[] actions)
    {
        this.actions = actions;
        this.playersStates = new int[playersNumber];
    }

    public bool ProcessInput(PlayerIndex playerNumber, Player.ActionInput input)
    {
        var playerState = playersStates[(int)playerNumber];
        if (actions[playerState].input == input)
        {
            playersStates[(int)playerNumber] = playerState + 1;
        }
        return false;
    }

    public int[] UnfaithfulPlayers()
    {
        List<int> unfaithfulPlayers = new List<int>();

        for (int i = 0; i < playersStates.Length; i++)
        {
            if(playersStates[i] < actions.Length)
            {
                unfaithfulPlayers.Add(i+1);
            }
        }
        return unfaithfulPlayers.ToArray();
    }

    internal IEnumerable<Action> GetActions()
    {
        return actions;
    }
}
