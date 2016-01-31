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
    public GameObject lightning;

    private Dictionary<PlayerIndex, Player> players;

    [SerializeField]
    private Malus[] maluses;
    private RandomMalusGenerator rmg;

    [SerializeField]
    private Action[] actions;

    [SerializeField]
    private ZoneOrder.Zone[] zones;
    private RandomRitualGenerator rrg;


    public Canvas zonePopups;
    private Animator animator;
    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        players = new Dictionary<PlayerIndex, Player>();

        var rng = new System.Random();
        var playerIndexes = GameManager.GetInstance().players;

        players = new Dictionary<PlayerIndex, Player>();

        rmg = new RandomMalusGenerator(maluses);

        rrg = new RandomRitualGenerator(players, actions, zones);
        nextRitual = rrg.GetRandomRitual();
        explainRitual(nextRitual);

        var npcs = GameObject.FindGameObjectsWithTag("NPC");
        var pickedNPCs = new List<GameObject>();
        foreach (var playerIndex in playerIndexes)
        {
            GameObject npc;
            do
            {
                npc = npcs[rng.Next(0, npcs.Length)];

            } while (pickedNPCs.Contains(npc));

            pickedNPCs.Add(npc);
            Humanify(npc, playerIndex);
            players.Add(playerIndex, npc.GetComponent<Player>());
        }
    }

    private void Humanify(GameObject npc, PlayerIndex playerIndex)
    {
        Destroy(npc.gameObject.transform.FindChild("targetPos").gameObject);
        var controller = npc.gameObject.transform.FindChild("AIThirdPersonController");
        Destroy(controller.GetComponent<IABehavior>());
        Destroy(controller.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>());
        Destroy(controller.GetComponent<NavMeshAgent>());

        var player = npc.AddComponent<Player>();
        player.playerIndex = playerIndex;
        npc.tag = "Player";

        var userController = controller.gameObject.AddComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>();
        userController.inversion = 1;
        userController.speed = 3;
        userController.playerIndex = playerIndex;
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

                animator.SetTrigger("Clap");
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
                ritualDuration = currentRitual.duration;
                timeSinceRitualBegin = 0;
                animator.SetTrigger("Clap");
                StartRitual(currentRitual);
            }
        }
    }

    public void ProcessPlayerInput(PlayerIndex playerNumber, Player.ActionName input)
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

        // playerPos = player.transform.GetChild(0).transform.position;

        GameObject.Instantiate(lightning, player.gameObject.transform.position, Quaternion.identity);

    }

    private void PunishUnfaithfulPlayers()
    {
        foreach (var player in currentRitual.UnfaithfulPlayers())
            PunishPlayer(player);
    }


    public void StartRitual(Ritual ritual)
    {
        var npcs = GameObject.FindGameObjectsWithTag("NPC");
        foreach (var npc in npcs)
        {
            npc.GetComponentInChildren<IABehavior>().startPraying(ritual);
        }
    }
    private void explainRitual(Ritual ritual)
    {
        if(ritual.GetType() == typeof(ActionSequence))
        {
            var seq = (ActionSequence)ritual;
            StartCoroutine(ExplainSequence(seq.actions, 0));
        } else
        {
            var zo = (ZoneOrder)ritual;
            StartCoroutine(ExplainZone(zo.zone, zo.isAllowed));
        }
        ritual.Explain();
    }

    IEnumerator ExplainSequence(Action[] actions, int index)
    {
        yield return new WaitForSeconds((ritualTempo - 1) / (actions.Length + 1));

        animator.SetTrigger(actions[index].actionName.ToString());

        index++;

        if (index >= actions.Length)
            yield break;

        yield return StartCoroutine(ExplainSequence(actions,index));
    }

    IEnumerator ExplainZone(ZoneOrder.Zone zone, bool allowed)
    {
        yield return new WaitForSeconds((ritualTempo - 1) / 2);

        var name = allowed ? "Marcher_" : "Nogo_";
        name += zone.ToString();

        var popup = zonePopups.transform.FindChild(name);
        popup.gameObject.SetActive(true);
        yield return new WaitForSeconds((ritualTempo - 1) / 2);
        popup.gameObject.SetActive(false);


        yield break;
    }
}
