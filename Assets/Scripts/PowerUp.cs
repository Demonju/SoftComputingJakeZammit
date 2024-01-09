using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PowerUp : MonoBehaviour
{
    public float fleeDuration = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Cancel any existing ActivatePowerUp coroutine
            StopAllCoroutines();

            StartCoroutine(ActivatePowerUp());
            Destroy(gameObject);
        }
    }

    private IEnumerator ActivatePowerUp()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        foreach (Enemy enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.GhostState = GhostStateMachine.FLEEING_PLAYER;

                NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();
                if (agent != null)
                {
                    agent.destination = enemy.GetNextDestination().transform.position;
                }
            }
        }

        yield return new WaitForSeconds(fleeDuration);

        foreach (Enemy enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.GhostState = GhostStateMachine.PATROLLING_WAYPOINTS;

                NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();
                if (agent != null)
                {
                    agent.destination = enemy.GetNextDestination().transform.position;
                }
            }
        }
    }
}

