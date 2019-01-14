using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAScript : MonoBehaviour {

    public Collider AreaOfHit;
    public float hitCooldown;
    public float speed;
    public float health;
    public float mana;
    public float hitDamage;
    public string enemyName;

    private GameObject m_target;
    private NavMeshAgent m_agent;

	// Use this for initialization
	void Start () {
        m_agent = GetComponent<NavMeshAgent>();
		m_target = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update ()
    {
        //follow target
        if (m_target != null)
        {
            m_agent.speed = speed;
            m_agent.SetDestination(m_target.transform.position);
        }
    }

}
