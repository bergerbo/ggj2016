using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomRitualGenerator {

    private int playersNumber;
    private System.Random rng;

    public RandomRitualGenerator(int playersNumber)
    {
        this.playersNumber = playersNumber;
        rng = new System.Random();
    }

    public Ritual GetRandomRitual()
    {
        int numberOfActions = rng.Next(1, 4);
        var actions = new List<Action>();

        for(int i = 0;i<numberOfActions;i++)
        {
            var action = new Action();
            action.input = (Player.ActionInput)rng.Next(0, 4);
            actions.Add(action);
        }

        return new ActionSequence(playersNumber,actions.ToArray());
    }

}
