using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using XInputDotNetPure;

public class God : MonoBehaviour
{
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

    [SerializeField]
    private Action[] actions;
    [SerializeField]
    private ZoneOrder.Zone[] zones;
    private RandomRitualGenerator rrg;


    // Use this for initialization
    void Start()
    {
        players = new Dictionary<PlayerIndex, Player>();

        var playerObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (var po in playerObjects)
        {
            var player = po.GetComponent<Player>();
            players.Add(player.playerIndex, player);
        }

        rmg = new RandomMalusGenerator(maluses);

        rrg = new RandomRitualGenerator(players,actions,zones);
        nextRitual = rrg.GetRandomRitual();
        explainRitual(nextRitual);
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;

        if (currentRitual != null)
        {
            timeSinceRitualBegin += dt;

            if (timeSinceRitualBegin >= ritualDuration)
            {
                PunishUnfaithfulPlayers();
                currentRitual = null;
                nextRitual = rrg.GetRandomRitual();
                explainRitual(nextRitual);
            }
        }
        else
        {
            timeSinceLastRitual += dt;

            if (timeSinceLastRitual >= ritualTempo)
            {
                timeSinceLastRitual -= ritualTempo;
                currentRitual = nextRitual;
                timeSinceRitualBegin = 0;
                Debug.Log("Go !");
                startRitual(currentRitual);
            }
        }
    }

    public void ProcessPlayerInput(PlayerIndex playerNumber, Player.ActionInput input)
    {
        Debug.Log("Player " + playerNumber + "pressed" + Enum.GetName(input.GetType(), input));
        var punishPlayer = true;
        if (currentRitual != null)
        {
            punishPlayer = currentRitual.ProcessInput(playerNumber, input);
        }
        if (punishPlayer)
            PunishPlayer(playerNumber);
    }

    private void PunishPlayer(PlayerIndex playerNumber)
    {
        Debug.Log("Punish Player " + playerNumber);

        var player = players[playerNumber];


        var currentMalus = player.GetComponentInChildren<Malus>();
        if (currentMalus != null)
        {
            currentMalus.Destroy();
        }

        var malus = rmg.GetRandomMalus();
        var instance = GameObject.Instantiate(malus);
        instance.gameObject.transform.SetParent(player.gameObject.transform);
    }

    private void PunishUnfaithfulPlayers()
    {
        foreach(var player in currentRitual.UnfaithfulPlayers())
            PunishPlayer(player);     
    }


    public delegate void StartRitual(Ritual ritual);
    public event StartRitual startRitual;

    private void explainRitual(Ritual ritual)
    {
        ritual.Explain();
    }
}
