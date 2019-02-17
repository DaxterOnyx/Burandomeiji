using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Character))]
[RequireComponent(typeof(EnemyStats))]
[RequireComponent(typeof(DoHits))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
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
    public Rigidbody parentrb;
    Rigidbody[] corpse;
    bool isAnimated = true;
    Animator animator;

    private void Awake()
    {
        // get the components on the object we need ( should not be null due to require component so no need to check )
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        character = GetComponent<Character>();
        enemyStats = GetComponent<EnemyStats>();
        doHits = GetComponent<DoHits>();
        parentrb = GetComponent<Rigidbody>();
        corpse = GetComponentsInChildren<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        agent.updateRotation = false;
        agent.updatePosition = true;
        agent.speed = enemyStats.speed;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
            takeHitsTarget = target.gameObject.GetComponentInChildren<TakeHits>();
            agent.SetDestination(target.position);
        }

        foreach (Rigidbody rb in corpse)
        {
            rb.isKinematic = true;
        }
    }

    
    private void Update()
    {
        if(isAnimated)
        {
            if (isFreeze)
            {
                character.Move(Vector3.zero, false, false, false);
                agent.SetDestination(this.gameObject.transform.position);
                target = null;
            }
            else
            {
                if (target == null)
                {
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    if (player != null)
                    {
                        target = player.transform;
                        takeHitsTarget = target.gameObject.GetComponentInChildren<TakeHits>();
                        agent.SetDestination(target.position);
                    }
                }
                else
                {
                    distanceFly = Vector3.Distance(target.position, this.transform.position) - 1f;

                    if (enemyStats.type == EnemyStats.enemyType.Melee || enemyStats.type == EnemyStats.enemyType.Boss)
                    {
                        if (agent.remainingDistance < agent.stoppingDistance && distanceFly < agent.stoppingDistance)
                        {
                            Attack();
                        }
                        else
                        {
                            Move();
                        }
                    }
                    else
                    {
                        if (distanceFly < agent.stoppingDistance) // Si la distance est plus petit ou �gal � stoppingDistance
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
        }
    }

    public void SetCorpseAnimated(bool active)
    {
        isAnimated = active;
        agent.enabled = active;
        animator.enabled = active;
        parentrb.isKinematic = !active;

        foreach (Rigidbody rb in corpse)
        {
            rb.isKinematic = active;
            rb.detectCollisions = !active;
        }
    }

    public void Die()
    {
        SetCorpseAnimated(false);
        Destroy(this.gameObject, 10f);
    }

    public void SetTarget(Transform _target)
    {
        this.target = _target;
    }

    private void Attack()
    {
        agent.SetDestination(this.gameObject.transform.position);
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
        character.AnimSpeedMultiplier = _level / 100f;
    }

    public void UnSlow()
    {
        agent.speed = enemyStats.speed;
        character.AnimSpeedMultiplier = 1f;
    }
}

