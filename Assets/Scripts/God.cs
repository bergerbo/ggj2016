using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using XInputDotNetPure;

public class God : MonoBehaviour {


    public Ritual currentRitual;
    public Ritual nextRitual;

    public float ritualTempo;
    public float ritualDuration;

    private float timeSinceRitualBegin;
    private float timeSinceLastRitual;
    
    private Dictionary<PlayerIndex, Player> players;

    [SerializeField]
    private Malus[] maluses;
    private RandomMalusGenerator rmg;

    private Action[] actions;
    private RandomRitualGenerator rrg;


    // Use this for initialization
    void Start () {
        rrg = new RandomRitualGenerator(1);
        nextRitual = rrg.GetRandomRitual();
        BroadcastRitual(nextRitual);

        rmg = new RandomMalusGenerator(maluses);

        players = new Dictionary<PlayerIndex, Player>();

        var playerObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach(var po in playerObjects) {
            var player = po.GetComponent<Player>();
            players.Add(player.playerIndex, player);
        }

    }

    // Update is called once per frame
    void Update () {
        float dt = Time.deltaTime;

        if(currentRitual != null)
        {
            timeSinceRitualBegin += dt;

            if(timeSinceRitualBegin >= ritualDuration)
            {
                PunishUnfaithfulPlayers();
                currentRitual = null;
                nextRitual = rrg.GetRandomRitual();
                BroadcastRitual(nextRitual);
            }
        } else
        {
            timeSinceLastRitual += dt;

            if (timeSinceLastRitual >= ritualTempo)
            {
                timeSinceLastRitual -= ritualTempo;
                currentRitual = nextRitual;
                timeSinceRitualBegin = 0;
                Debug.Log("Go !");
            }
        }
    }

    public void ProcessPlayerInput(PlayerIndex playerNumber, Player.ActionInput input)
    {
        Debug.Log("Player " + playerNumber + "pressed" + Enum.GetName(input.GetType(), input));
        var punishPlayer = true;
        if(currentRitual != null)
        {
            punishPlayer = !currentRitual.ProcessInput(playerNumber, input);
        }
        if (punishPlayer)
            PunishPlayer(playerNumber);
    }

    private void PunishPlayer(PlayerIndex playerNumber)
    {
        Debug.Log("Punish Player " + playerNumber);
        var player = players[playerNumber];
        var malus = rmg.GetRandomMalus();
        player.gameObject.AddComponent(malus);
    }

    private void PunishUnfaithfulPlayers()
    {
        var playersToPunish = currentRitual.UnfaithfulPlayers();

        Debug.Log(playersToPunish.Length + " players to punish for ritual uncompletion");
    }

    private void BroadcastRitual(Ritual ritual)
    {
        var str = "For ritual next press buttons:";
        ActionSequence seq = (ActionSequence)ritual;

        foreach(Action action in seq.GetActions())
        {
            str += " " + Enum.GetName(action.input.GetType(),action.input);
        }

        Debug.Log(str);
    }
}
