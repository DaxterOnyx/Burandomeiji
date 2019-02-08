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
    EnemyStats enemyStats;
    private float distanceFly;
    private bool isAttacking;
    public bool isFreeze = false;

    private void Start()
    {
        // get the components on the object we need ( should not be null due to require component so no need to check )
        agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
        character = GetComponent<Character>();
        enemyStats = GetComponent<EnemyStats>();
        doHits = GetComponent<DoHits>();

        agent.updateRotation = false;
        agent.updatePosition = true;
        agent.speed = enemyStats.speed;
    
        target = GameObject.FindGameObjectWithTag("Player").transform;
        takeHitsTarget = target.gameObject.GetComponentInChildren<TakeHits>();
        
        if(target != null)
        {
            agent.SetDestination(target.position);
        }      
    }

    
    private void Update()
    {
        if(isFreeze)
        {
            character.Move(Vector3.zero, false, false, false);
        }
        else
        {
            if (target == null)
            {
                target = GameObject.FindGameObjectWithTag("Player").transform;
                agent.SetDestination(target.position);
            }

            distanceFly = Vector3.Distance(target.position, this.transform.position) - 1f;

            if (enemyStats.type == EnemyStats.enemyType.Melee || enemyStats.type == EnemyStats.enemyType.Boss)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if(distanceFly < 5f)
                    {
                        Attack();
                    }
                    else
                    {
                        Move();
                    }
                    
                    agent.SetDestination(target.position);
                }
                else
                {
                    Move();
                }
            }
            else
            {
                if (distanceFly <= agent.stoppingDistance) // Si la distance est plus petit ou égal à stoppingDistance
                {

                    Attack();
                }
                else
                {
                    Move();
                }

            }
        }
        
    }

    public void SetTarget(Transform _target)
    {
        this.target = _target;
    }

    private void Attack()
    {
        character.Move(Vector3.zero, false, false, true);
        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        doHits.Attack(takeHitsTarget);
            
    }

    private void Move()
    {
        agent.SetDestination(target.position);
        character.Move(agent.desiredVelocity, false, false, false);
    }

    public void Slow(float _level)
    {
        agent.speed -= agent.speed * (_level / 100f);
    }

    public void UnSlow()
    {
        agent.speed = enemyStats.speed;
    }
}

