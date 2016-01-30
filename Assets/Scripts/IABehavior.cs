using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;
using System;

public class IABehavior : MonoBehaviour {

	public enum State {
		WANDER,
		STAND,
		LEAVE,
		PRAY,
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

	private Ritual currentRitual;
	private NavMeshAgent navMeshAgent;
	private ThirdPersonCharacter character;
	private AICharacterControl characterControl;



	void Start () 
	{	
		// state = State.LEAVE;
		// GameObject god = GameObject.Find("God");
		// God godScript = god.GetComponent<God>();
		// God.startRitual += new EventHandler(Pray);

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
		while(timer < time)
		{	
			timer += Time.deltaTime;
			yield return null;
		}
		
		if(state == State.WANDER)
			GetRandomPosition(mapCollider);
		else if(state == State.PRAY)
			GetRandomPosition(specificAreaCollider);
		yield return StartCoroutine(GetTargetLoop(UnityEngine.Random.Range(1f, 2f) * 2f));

	}

	IEnumerator IAStateMachine()
	{
		switch (state)
		{
			case State.WANDER:
				GetRandomPosition(mapCollider);
				Wander();
				break;
			case State.STAND:
				Stand();
				break;
			case State.LEAVE:
				Leave();
				break;
			case State.PRAY:
				yield return new WaitForSeconds(UnityEngine.Random.Range(0f, 2f));
				Pray();
				break;
		}
		yield return null;
	} 	
	

	public void Wander()
	{
		// character.Move(IAtargetPosition.transform.position, true, false);
		characterControl.target = IAtargetPosition.transform;

	}

	public void Stand()
	{

	}

	public void Leave()
	{
		Debug.Log("leave");
		StopCoroutine("GetTargetLoop");
		characterControl.target = outerPoint.transform;
	}

	public void Pray()
	{
		if(currentRitual.GetType() == typeof(ActionSequence))
		{
			executeSequence((ActionSequence)currentRitual);
		}
		else if(currentRitual.GetType() == typeof(ZoneOrder))
		{
			getClosestSpecificArea((ZoneOrder)currentRitual);
		}

		// Animation.Play(anim);
	}
	public void startPraying(Ritual ritual){
		currentRitual = ritual;
		state = State.PRAY;
	}

	public void executeSequence(ActionSequence ritual){
		Action[] actions = ritual.actions;
		

	}

	public void getClosestSpecificArea(ZoneOrder ritual){
		if(ritual.isAllowed)
		{
			GameObject[] zones = GameObject.FindGameObjectsWithTag(ritual.zone.ToString());
			GameObject closest = null;
			float min = float.MaxValue;
			for(int i = 0; i<zones.Length; i++)
			{
				var distance = Vector3.Distance(gameObject.transform.position, zones[i].transform.position);
				if(distance < min){
					min = distance;
					closest = zones[i];
				}
			}
			specificAreaCollider = closest.GetComponent<Collider>();
		}
	}

	public void GetRandomPosition(Collider area)
	{
		// Debug.Log("rand");
		float x= UnityEngine.Random.Range(mapCollider.bounds.center.x - mapCollider.bounds.extents.x, mapCollider.bounds.center.x + mapCollider.bounds.extents.x);
		float z= UnityEngine.Random.Range(mapCollider.bounds.center.z - mapCollider.bounds.extents.z, mapCollider.bounds.center.z + mapCollider.bounds.extents.z);
		IAtargetPosition.transform.position = new Vector3(x, 0, z);
	}
	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(gameObject.transform.position, IAtargetPosition.transform.position)< 3f)
			GetRandomPosition(mapCollider);
	}
}
