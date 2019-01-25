using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Character))]
[RequireComponent(typeof(EnemyStats))]
public class AIScript : MonoBehaviour
{
    public NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
    public Character character { get; private set; } // the character we are controlling
    public Transform target;                    // target to aim for
    [SerializeField] private Collider col;
    DoHits doHits;

    private void Start()
    {
        // get the components on the object we need ( should not be null due to require component so no need to check )
        agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
        character = GetComponent<Character>();

        agent.updateRotation = false;
        agent.updatePosition = true;

        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent.speed = GetComponent<EnemyStats>().speed;
        doHits = col.GetComponent<DoHits>();
    }


    private void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
        } 

        if (agent.remainingDistance > agent.stoppingDistance)
        {
            character.Move(agent.desiredVelocity, false, false);
        }
        else
        {
            character.Move(Vector3.zero, false, false);
        }     
    }


    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}

