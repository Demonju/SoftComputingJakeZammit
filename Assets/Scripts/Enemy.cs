using System;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public enum GhostStateMachine
{
    IDLE,
    PATROLLING_WAYPOINTS,
    CHASING_PLAYER,
    ATTACKING_PLAYER,
    FLEEING_PLAYER
}

public class Enemy : MonoBehaviour
{
    private GameObject _destination;
    private GameObject[] _waypoints;
    private NavMeshAgent _agent;
    private GameObject _player;
    [SerializeField] private GhostStateMachine ghostState = GhostStateMachine.PATROLLING_WAYPOINTS;


    public GhostStateMachine GhostState
    {
        get => ghostState;
        set
        {
            ghostState = value;
            switch (ghostState)
            {
                case GhostStateMachine.PATROLLING_WAYPOINTS:
                    _agent.speed = 3f;
                    break;
                case GhostStateMachine.CHASING_PLAYER:
                    _agent.speed = 5f;
                    break;
                case GhostStateMachine.FLEEING_PLAYER:
                    _agent.speed = 5f;
                    break;
                case GhostStateMachine.ATTACKING_PLAYER:
                    break;
                default:
                    _agent.speed = 0;
                    break;
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _agent = GetComponent<NavMeshAgent>();
        _waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        _agent.destination = GetNextDestination().transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_agent == null || _player == null)
        {
            // Handle the case where _agent or _player is null
            return;
        }

        // Zombie has closed in on the player, so start attacking him
        if (GhostState == GhostStateMachine.CHASING_PLAYER && _agent.hasPath && _agent.remainingDistance <= 3)
        {
            // Transition to attacking state
            GhostState = GhostStateMachine.ATTACKING_PLAYER;
            _agent.destination = _player.transform.position;
        }
        // Zombie is chasing player, so update the destination to the current player position
        else if (GhostState == GhostStateMachine.CHASING_PLAYER)
        {
            // Continue chasing the player
            _agent.destination = _player.transform.position;
            // Keep attacking player while chasing him (add your attacking logic here)
        }
        else if (GhostState == GhostStateMachine.ATTACKING_PLAYER)
        {
            // Check if the player is out of attack range, transition back to chasing
            if (_agent.hasPath && _agent.remainingDistance >= 3)
            {
                GhostState = GhostStateMachine.CHASING_PLAYER;
                _agent.destination = _player.transform.position;
            }
            else
            {
                // Continue attacking the player
                _agent.destination = _player.transform.position;
            }
        }
        // Zombie arrived at waypoint, go to a different waypoint
        else if (GhostState == GhostStateMachine.PATROLLING_WAYPOINTS && _agent.hasPath && _agent.remainingDistance <= 2)
        {
            _agent.destination = GetNextDestination().transform.position;
        }
    }

    private GameObject GetNextDestination()
    {
        GameObject waypoint;
        //Checking if the new waypoint is different than the previous one
        do
        {
            waypoint = _waypoints[UnityEngine.Random.Range(0, _waypoints.Length)];
        } while (waypoint == _destination);
        _destination = waypoint;
        return _destination;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GhostState = GhostStateMachine.CHASING_PLAYER;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GhostState = GhostStateMachine.PATROLLING_WAYPOINTS;
            _agent.destination = GetNextDestination().transform.position;
        }
    }
}
