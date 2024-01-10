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
            Debug.Log("Power-up picked up by player"); // Add this line for debugging

            // Trigger the fleeing behavior in enemies
            StartCoroutine(ActivatePowerUp());

            // Optional: Add other power-up effects or logic
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
                Debug.Log("Enemy started fleeing"); // Add this line for debugging

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
                Debug.Log("Enemy stopped fleeing"); // Add this line for debugging

                // Only transition back to patrolling if currently in the fleeing state
                if (enemy.GhostState == GhostStateMachine.FLEEING_PLAYER)
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

}

