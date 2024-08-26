using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Animator))]
    public class AIController : MonoBehaviour
    {
        public enum AIState
        {
            Idle,
            Patrol,
            Wander,
            Stun,
            Attack,
            Chase,
        }
        #region Variables
        //the current state - going to need states
        [SerializeField] AIState _state = AIState.Idle;
        //Nav Mesh Agent 
        [SerializeField] private NavMeshAgent _agent;
        //Animator
        [SerializeField] private Animator _animator;
        //walk speed and a run/chase speed
        [SerializeField] private float _walkSpeed = 2f, _runSpeed = 5;
        //patrolPoints/wayPoints []array of locations
        [SerializeField] private Transform[] _wayPoints;
        //iteration of array
        [SerializeField] private int _currentWayPointIndex = 0;
        // move rendo
        [SerializeField] private Vector3 _randomPosition;
        //Where are you Player??
        [SerializeField] private float _detectionRadius = 10f;
        //Who are you Player??
        [SerializeField] private LayerMask _playerLayer;
        //keep chasing distance
        [SerializeField] private float _chaseDistance = 20f;
        //attack distance
        [SerializeField] private float _attackDistance = 20f;
        #endregion

        #region Unity Event Functions
        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            _state = AIState.Idle;
            Transitiontostate(_state);
        }
        #endregion
        private void Update()
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, _detectionRadius, _playerLayer);
            if (hitColliders.Length > 0)
            {
                Transitiontostate(AIState.Chase);
            }
            //Transitiontostate(_state);
        }


        #region States
        void Transitiontostate(AIState newstate)
        {
            _state = newstate;
            switch (_state)
            {
                case AIState.Idle:
                    StartCoroutine(Idle());
                    break;
                case AIState.Patrol:
                    Patrol();
                    break;
                case AIState.Wander:
                    Wander();
                    break;
                case AIState.Stun:
                    Stun();
                    break;
                case AIState.Attack:
                    Attack();
                    break;
                case AIState.Chase:
                    Chase();
                    break;
                default:
                    Idle();
                    break;
                    

            }
        }
        IEnumerator Idle()
        {
            PlayAnim("Idle");
            yield return new WaitForSeconds(Random.Range(3, 10f));
            //yield return new WaitForSeconds(timer);
            
            Debug.Log("Timer Complete");

            if (_state == AIState.Idle)
            {
                int choice = Random.Range(0, 2);
                if (choice == 0)
                {
                    //Debug.Log("Wander")
                    _randomPosition = GetRandomPosition();
                    Transitiontostate(AIState.Wander);
                }
                else
                {
                    //Debug.Log("Patrol")
                    Transitiontostate(AIState.Patrol);
                }
            }
           
        }
        void Patrol()
        {
            _agent.speed = _walkSpeed;
            PlayAnim("Walk");
            if (_wayPoints.Length == 0)
            {
                Debug.Log("No Partol waypoints assingned");
                Transitiontostate(AIState.Idle);
            }
            if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
            {
                //Debug.Log("");

               int choice = Random.Range(0, 2);
                if (choice == 0)
                {
                    Transitiontostate(AIState.Idle);
                }
                else
                {
                    TransitionToNextWayPoint();
                }
            }
        }
        void TransitionToNextWayPoint()
        { 
            _currentWayPointIndex = (_currentWayPointIndex +1) % _wayPoints.Length;
            _agent.SetDestination(_wayPoints[_currentWayPointIndex].position);
            _agent.speed = _walkSpeed;
            PlayAnim("Walk");
        }

        void Wander()
        {
            _agent.SetDestination(_randomPosition);
            _agent.speed = _walkSpeed;
            PlayAnim("Walk");
            if (_agent.remainingDistance <= 0.1f)
            {
                int choice = Random.Range(0, 2);
                if (choice == 0)
                {
                    _randomPosition = GetRandomPosition();
                    Transitiontostate(AIState.Wander);
                }
                else
                {
                    Transitiontostate(AIState.Idle);
                }
            }
        }
        Vector3 GetRandomPosition()
        {
            Vector3 finalPosition = Vector3.zero;
            Vector3 randomDirection = Random.insideUnitSphere * 10;
            randomDirection += transform.position;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, 10, 1))
            { 
                finalPosition = hit.position;
            }
            return finalPosition;
        }
        void Chase()
        {
            if (!_agent.pathPending && _agent.remainingDistance <= _chaseDistance)
            {
                PlayAnim("Run");
                _agent.speed = _runSpeed;
                _agent.SetDestination(GetPlayerPosition());
            }
            else 
            {
                _randomPosition = GetRandomPosition();
                Transitiontostate(AIState.Wander);
            }
        }
        void Stun()
        {

        }
        void Attack()
        {

        }

        #endregion
        void PlayAnim(string trigger)
        {
            _animator.SetTrigger(trigger);
        }
        private Vector3 GetPlayerPosition()
        { 
            return GameObject.FindWithTag("Player").transform.position;
        }

    }


}


