using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class swarmScript : MonoBehaviour
{
    public Transform player; // Reference to the player object
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            // Set the destination to the player's current position
            agent.SetDestination(player.position);
        }
    }
}
