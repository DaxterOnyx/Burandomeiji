using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAScript : MonoBehaviour {

    [Header("Attack")]
    public Collider AreaOfHit;
    public float HitCouldown;
    public float HitDamage;

    private GameObject m_target;
    private NavMeshAgent m_agent;

	// Use this for initialization
	void Start () {
        m_agent = GetComponent<NavMeshAgent>();
		m_target = GameObject.Find("VRTK");
    }
	
	// Update is called once per frame
	void Update ()
    {
		//TODO hit if to close of target

		//follow target
		m_agent.SetDestination(m_target.transform.position);
    }

}
