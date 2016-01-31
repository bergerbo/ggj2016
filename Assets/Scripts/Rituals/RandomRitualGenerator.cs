using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;
using System.Linq;

public class RandomRitualGenerator {

    private Dictionary<PlayerIndex, Player> players;
    private System.Random rng;

    private Action[] actions;
    private ZoneOrder.Zone[] zones;

    public RandomRitualGenerator(Dictionary<PlayerIndex,Player> players, Action[] actions, ZoneOrder.Zone[] zones)
    {
        rng = new System.Random();

        this.players = players;
        this.actions = actions;
        this.zones = zones;
    }

    public Ritual GetRandomRitual()
    {
        var selecta = rng.NextDouble();
        if(selecta > 0.0)
        {
           return GetRandomZoneOrder();
        } 
 
        return GetRandomActionSequence();
    }

    public ActionSequence GetRandomActionSequence()
    {
        int numberOfActions = rng.Next(1, 4);
        var actionsForRitual = new List<Action>();

        for (int i = 0; i < numberOfActions; i++)
        {
            var action = actions[rng.Next(0, actions.Length)];
            actionsForRitual.Add(action);
        }

        return new ActionSequence(players.Keys.ToArray(), actionsForRitual.ToArray());

    }

    public ZoneOrder GetRandomZoneOrder()
    {
        bool allowed = rng.Next(0, 2) > 0;
        var zone = zones[rng.Next(0, zones.Length)];
        return new ZoneOrder(zone, allowed, players);
    }

}
