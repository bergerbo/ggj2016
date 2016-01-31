using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;
using System;

public class IABehavior : MonoBehaviour
{

    public enum State
    {
        WANDER,
        STAND,
        LEAVE,
        PRAY,
        OBEY,
    }

    public State state;
    // Use this for initialization

    private GameObject map;
    private Collider mapCollider;

    private GameObject specificArea;
    private Collider specificAreaCollider;
    public GameObject IAtargetPosition;

    public GameObject outerPoint;

    private float timer;
    private float time;
    private float prayingTimer;

    private Ritual currentRitual;
    private NavMeshAgent navMeshAgent;
    private ThirdPersonCharacter character;
    private AICharacterControl characterControl;
    private Action[] actions;
    private float spareTime;
    private int currentAction;

    void Start()
    {
        // state = State.LEAVE;
        // GameObject god = GameObject.Find("God");
        // God godScript = god.GetComponent<God>();
        // God.startRitual += new EventHandler(Pray);
        state = State.WANDER;
        outerPoint = GameObject.Find("OuterPoint");
        map = GameObject.Find("map");
        mapCollider = map.GetComponent<Collider>();

        navMeshAgent = GetComponent<NavMeshAgent>();
        character = GetComponent<ThirdPersonCharacter>();
        characterControl = GetComponent<AICharacterControl>();

        StartCoroutine(GetTargetLoop(UnityEngine.Random.Range(1f, 2f) * 2f));
        StartCoroutine(IAStateMachine());
    }

    IEnumerator GetTargetLoop(float time)
    {
        timer = 0;
        while (timer < time)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        if (state == State.WANDER)
        {
            GetRandomPosition(mapCollider);
            characterControl.target = IAtargetPosition.transform;
        }

        yield return StartCoroutine(GetTargetLoop(UnityEngine.Random.Range(1f, 2f) * 2f));

    }

    IEnumerator IAStateMachine()
    {
        while (true)
        {
            switch (state)
            {
                case State.WANDER:
                    break;
                case State.STAND:
                    break;
                case State.LEAVE:
                    break;
                case State.PRAY:
                    if (currentAction >= actions.Length)
                    {
                        navMeshAgent.Resume();
                        state = State.WANDER;
                    }
                    else
                    {
                        while (character.isInAnimation())
                            yield return null;

                        var action = actions[currentAction];
                        character.TriggerAction(action.actionName.ToString());
                        currentAction++;
                        yield return new WaitForSeconds(action.duration + 0.1f);
                    }
                    break;
                case State.OBEY:
                    Debug.Log("OBEY");
                    state = State.WANDER;
                    yield return new WaitForSeconds(currentRitual.duration);
                    break;
            }
            yield return null;
        }
    }


    public void Wander()
    {
        // character.Move(IAtargetPosition.transform.position, true, false);

    }

    public void Stand()
    {

    }

    public void Leave()
    {
        Debug.Log("leave");
        //StopCoroutine("GetTargetLoop");
        characterControl.target = outerPoint.transform;
    }

    public void Pray()
    {
        //	if(currentRitual.GetType() == typeof(ActionSequence))
        //	{
        //		executeSequence((ActionSequence)currentRitual);
        //	}
        //	else if(currentRitual.GetType() == typeof(ZoneOrder))
        //	{
        //		getClosestSpecificArea((ZoneOrder)currentRitual);
        //	}
    }

    public void startPraying(Ritual ritual)
    {
        currentRitual = ritual;

        if (ritual.GetType() == typeof(ActionSequence))
        {
            navMeshAgent.Stop();
            //IAtargetPosition.transform.position = character.transform.position;
            state = State.PRAY;
            actions = ((ActionSequence)ritual).actions;

            currentAction = 0;
            spareTime = ritual.duration;
            foreach (var action in actions)
            {
                spareTime += action.duration;
            }
        }
        else
        {
            state = State.OBEY;
            getClosestSpecificArea((ZoneOrder)ritual);
        }
    }

    public void executeSequence(ActionSequence ritual)
    {
        Action[] actions = ritual.actions;
    }

    public void getClosestSpecificArea(ZoneOrder ritual)
    {
        if (ritual.isAllowed)
        {
            GameObject[] zones = GameObject.FindGameObjectsWithTag(ritual.zone.ToString());
            GameObject closest = null;
            float min = float.MaxValue;
            for (int i = 0; i < zones.Length; i++)
            {
                var distance = Vector3.Distance(gameObject.transform.position, zones[i].transform.position);
                if (distance < min)
                {
                    min = distance;
                    closest = zones[i];
                }
            }

            IAtargetPosition.transform.position = closest.transform.position;
        }
    }

    public void GetRandomPosition(Collider area)
    {
        // Debug.Log("rand");
        float x = UnityEngine.Random.Range(mapCollider.bounds.center.x - mapCollider.bounds.extents.x, mapCollider.bounds.center.x + mapCollider.bounds.extents.x);
        float z = UnityEngine.Random.Range(mapCollider.bounds.center.z - mapCollider.bounds.extents.z, mapCollider.bounds.center.z + mapCollider.bounds.extents.z);
        IAtargetPosition.transform.position = new Vector3(x, 0, z);
    }
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(gameObject.transform.position, IAtargetPosition.transform.position) < 3f)
            if (state == State.WANDER)
                GetRandomPosition(mapCollider);
        //else if (state == State.OBEY)
        //    GetRandomPosition(specificAreaCollider);

        //if (state == State.PRAY)
        //{
        //    if (prayingTimer >= currentRitual.duration)
        //    {
        //        state = State.WANDER;
        //    }
        //    prayingTimer += Time.deltaTime;
        //}
    }
}
