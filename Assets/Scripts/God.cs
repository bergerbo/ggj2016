using UnityEngine;
using System.Collections;
using System;

public class God : MonoBehaviour {


    public Ritual currentRitual;
    public Ritual nextRitual;

    public float ritualTempo;
    public float ritualDuration;

    private float timeSinceRitualBegin;
    private float timeSinceLastRitual;

    private RandomRitualGenerator rrg;


    // Use this for initialization
    void Start () {
        rrg = new RandomRitualGenerator(1);
        nextRitual = rrg.GetRandomRitual();
        BroadcastRitual(nextRitual);
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

    public void ProcessPlayerInput(int playerNumber, Player.ActionInput input)
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

    private void PunishPlayer(int playerNumber)
    {
        Debug.Log("Punish Player " + playerNumber);
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
