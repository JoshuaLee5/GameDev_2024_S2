using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AIController2 : MonoBehaviour
{
    public enum NPCState { Idle, Wander, Patrol, Chase, Stun }
    [SerializeField]private NPCState currentState;

    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float movementSpeed = 3f;
    [SerializeField] private float chaseSpeed = 5f;
    [SerializeField] private int currentPatrolIndex;
    [SerializeField] private Vector3 randomPosition;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;
    [SerializeField] private float detectionRadius = 10f;
    [SerializeField] private float chaseDistance = 20f;
    [SerializeField] private LayerMask playerLayer;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        currentState = NPCState.Wander; // Default state
        TransitionToState(currentState);
    }

    void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, playerLayer);
        if (hitColliders.Length > 0)
        {
            Debug.Log("Player detected, starting chase!");
            TransitionToState(NPCState.Chase);
        }
        switch (currentState)
        {
            case NPCState.Idle:
                StartCoroutine(IdleRoutine());
                break;
            case NPCState.Wander:
                WanderBehavior();
                break;
            case NPCState.Patrol:
                PatrolBehavior();
                break;
            case NPCState.Chase:
                ChaseBehavior();
                break;
            case NPCState.Stun:
                StartCoroutine(StunRoutine());
                break;
        }
    }

    void TransitionToState(NPCState newState)
    {
        currentState = newState;
        switch (currentState)
        {
            case NPCState.Idle:
                StartCoroutine(IdleRoutine());
                break;
            case NPCState.Wander:
                WanderBehavior();
                break;
            case NPCState.Patrol:
                PatrolBehavior();
                break;
            case NPCState.Chase:
                ChaseBehavior();
                break;
            case NPCState.Stun:
                StartCoroutine(StunRoutine());
                break;
        }
    }

    IEnumerator IdleRoutine()
    {
        TriggerAnimation("Idle");


        yield return new WaitForSeconds(Random.Range(3f, 10f));


        if (currentState == NPCState.Idle)
        {
            int behaviour = Random.Range(0, 3);
            if (behaviour == 0)
            {
                randomPosition = GetRandomPosition();
                TransitionToState(NPCState.Wander); // or NPCState.Patrol
            }
            else
            {
                TransitionToState(NPCState.Patrol);
            }
        }
    }

    void WanderBehavior()
    {
        agent.SetDestination(randomPosition);
        agent.speed = movementSpeed;
        TriggerAnimation("Walk");
        if (agent.remainingDistance <= 0.1f)
        {
            int behaviour = Random.Range(0, 3);
            if (behaviour == 0)
            {
                randomPosition = GetRandomPosition();
            }
            else
            {
                TransitionToState(NPCState.Idle);
            }
        }
    }


    void PatrolBehavior()
    {

        agent.speed = movementSpeed;
        TriggerAnimation("Walk");
        if (patrolPoints.Length == 0)
        {
            Debug.LogError("No patrol waypoints assigned!");
            TransitionToState(NPCState.Idle);
        }

        // Set the agent's destination to the current waypoint
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (agent.pathStatus == NavMeshPathStatus.PathComplete)
            {
                int behaviour = Random.Range(0, 3);
                if (behaviour == 0)
                {
                    TransitionToState(NPCState.Idle);
                }
                else
                {
                    TransitionToNextWaypoint();
                }
            }
        }
    }
    void TransitionToNextWaypoint()
    {
        // Increment the waypoint index, loop back if necessary
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;

        // Set the agent's destination to the next waypoint
        agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        agent.speed = movementSpeed;
        TriggerAnimation("Walk");

    }
    void ChaseBehavior()
    {
        if (!agent.pathPending && agent.remainingDistance <= chaseDistance)
        {
            TriggerAnimation("Run");
            agent.speed = chaseSpeed;
            agent.SetDestination(GetPlayerPosition());
        }
        else
        {
            randomPosition = GetRandomPosition();
            TransitionToState(NPCState.Wander);
        }
            
    }

    IEnumerator StunRoutine()
    {
        agent.isStopped = true;
        TriggerAnimation("Stun");
        yield return new WaitForSeconds(3.5f);
        agent.isStopped = false;
        TransitionToState(currentState == NPCState.Chase ? NPCState.Chase : NPCState.Wander);
    }

    void TriggerAnimation(string trigger)
    {
        animator.SetTrigger(trigger);
    }

     Vector3 GetRandomPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 10; // Adjust this value as needed
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, 10, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

    Vector3 GetPlayerPosition()
    {
        // Replace with actual logic to get the player's position
        return GameObject.FindWithTag("Player").transform.position;
    }
}