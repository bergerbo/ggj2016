using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class ActionSequence : Ritual
{
    private Action[] actions;
    private int[] playersStates;

    public ActionSequence(int playersNumber, Action[] actions)
    {
        this.actions = actions;
        this.playersStates = new int[playersNumber];
    }

    public bool ProcessInput(int playerNumber, Player.ActionInput input)
    {
        var playerState = playersStates[playerNumber - 1];
        if (actions[playerState].input == input)
        {
            playersStates[playerNumber - 1] = playerState + 1;
            return true;
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
