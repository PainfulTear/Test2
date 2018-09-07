using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCBasicAI : MonoBehaviour {

    NavMeshAgent agent;
    public Transform checkpoint;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        Invoke("move", 5f);   
	}

    void move()
    {
        CheckPoint point = checkpoint.GetComponent<CheckPoint>();
        checkpoint = point.getRandom();
        agent.destination = checkpoint.position;
        Invoke("move", 5f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}


