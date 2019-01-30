using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Character))]
[RequireComponent(typeof(EnemyStats))]
[RequireComponent(typeof(DoHits))]
public class AIScript : MonoBehaviour
{
    public NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
    public Character character { get; private set; } // the character we are controlling
    public Transform target;                    // target to aim for
    DoHits doHits;
    TakeHits takeHitsTarget;
    private float distanceFly;
    private bool isAttacking;

    private void Start()
    {
        // get the components on the object we need ( should not be null due to require component so no need to check )
        agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
        character = GetComponent<Character>();

        agent.updateRotation = false;
        agent.updatePosition = true;
    
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent.speed = GetComponent<EnemyStats>().speed;
        doHits = GetComponent<DoHits>();
        takeHitsTarget = target.gameObject.GetComponentInChildren<TakeHits>();

    }

    
    private void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
        }

        distanceFly = Vector3.Distance(target.position, this.transform.position);

        if (distanceFly <= agent.stoppingDistance) // Si la distance est plus petit ou égal à stoppingDistance
        {
            Attack();
        }
        else
        {
            Move();
        }
    }

    public void SetTarget(Transform _target)
    {
        this.target = _target;
    }

    private void Attack()
    {
        character.Move(Vector3.zero, false, false);
        transform.LookAt(target);
        agent.SetDestination(this.transform.position);
        
        doHits.Attack(takeHitsTarget);
        isAttacking = true;     
    }

    private void Move()
    {
        if (isAttacking)
        {
            agent.SetDestination(target.position);
            isAttacking = false;
        }

        character.Move(agent.desiredVelocity, false, false);
    }

    //TODO pour Marc
    // Boolean animation attaque
}

