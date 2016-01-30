﻿using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

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

	public GameObject IAtargetPosition;

	private float timer;
	private float time;

	private NavMeshAgent navMeshAgent;
	private ThirdPersonCharacter character;
	private AICharacterControl characterControl;

	void Start () 
	{	

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
			
		GetRandomPosition(mapCollider);
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

	}

	public void Pray()
	{

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
