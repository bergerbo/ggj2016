using UnityEngine;
using System.Collections;
using System;

public class God : MonoBehaviour {


    public Ritual currentRitual;
    private Ritual nextRitual;

    public float ritualTempo;
    public float ritualDuration;

    private float timeSinceRitualBegin;
    private float timeSinceLastRitual;

    // Use this for initialization
    void Start () {
        nextRitual = new Ritual();
    }
	
	// Update is called once per frame
	void Update () {
        float dt = Time.deltaTime;
        timeSinceLastRitual += dt;

        if(timeSinceLastRitual >= ritualTempo)
        {
            timeSinceLastRitual -= ritualTempo;
            currentRitual = nextRitual;
            BroadcastRitual();
            timeSinceRitualBegin = 0;
            nextRitual = new Ritual();
        }

        if(currentRitual != null)
        {
            timeSinceRitualBegin += dt;

            if(timeSinceRitualBegin >= ritualDuration)
            {
                currentRitual = null;
                PunishUnfaithfulPlayers();

            }
        }


	}

    private void PunishUnfaithfulPlayers()
    {
        throw new NotImplementedException();
    }

    private void BroadcastRitual()
    {
        throw new NotImplementedException();
    }
}
