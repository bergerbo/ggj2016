using UnityEngine;
using System.Collections;
using System;
using XInputDotNetPure;
using System.Collections.Generic;
using System.Linq;

public class ZoneOrder : Ritual
{

    public enum Zone
    {
        Bois,
        Herbe,
        Mur,
        Pierre,
        Tapis,
        Terre
    }

    public Zone zone;
    public bool isAllowed;
    private Dictionary<PlayerIndex, Player> players;

    public ZoneOrder(Zone zone, bool isAllowed, Dictionary<PlayerIndex, Player> players)
    {
        this.isAllowed = isAllowed;
        this.zone = zone;
        this.players = players;
    }

    override public bool ProcessInput(PlayerIndex playerIndex, Player.ActionInput input)
    {
        return true;
    }

    override public IEnumerable<PlayerIndex> UnfaithfulPlayers()
    {
        if (isAllowed)
            return PlayersOutsideZone();
        else
            return PlayersInsideZone();
    }

    public IEnumerable<PlayerIndex> PlayersInsideZone()
    {
        foreach (var player in players)
        {
            IEnumerable<Zone> collisions = player.Value.GetZoneCollisions();
            if (collisions.Contains(zone))
            {
                yield return player.Key;
            }
        }
    }

    public IEnumerable<PlayerIndex> PlayersOutsideZone()
    {
        foreach (var player in players)
        {
            IEnumerable<Zone> collisions = player.Value.GetZoneCollisions();
            if (!collisions.Contains(zone))
            {
                yield return player.Key;
            }
        }
    }

    override public void Explain()
    {
        if (isAllowed)
        {
            Debug.Log("Players must go to " + zone);
        } else
        {
            Debug.Log("Players must leave to " + zone);
        }
    }
}
