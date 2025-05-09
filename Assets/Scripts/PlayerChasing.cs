using UnityEngine;
using UnityEngine.AI;

public class PlayerChasing : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    private NavMeshAgent agent; // Reference to the NavMeshAgent component for pathfinding
    private bool canSeePlayer = true; // Tracks whether the enemy can see the player
    private float defaultSpeed; // Stores the default speed of the enemy

    public float DefaultSpeed // Public getter for defaultSpeed
    {
        get { return defaultSpeed; }
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        defaultSpeed = agent.speed; // Save the default speed of the NavMeshAgent

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("Player object not found. Ensure the player has the 'Player' tag.");
        }
    }

    void Update()
    {
        // Only set the destination if the player is visible
        if (player != null && canSeePlayer)
        {
            agent.SetDestination(player.position);
        }
        
    }

    public void SetPlayerVisible(bool visible)
    {
        canSeePlayer = visible;

        if (agent != null)
        {
            if (!visible)
            {
                agent.ResetPath(); // Stop the NavMeshAgent's pathfinding
                agent.isStopped = true; // Stop the agent
                agent.speed = 0; // Set speed to 0 to stop movement
            }
            else
            {
                agent.isStopped = false; // Resume the agent
            }
        }
    }

    public void SetAgentSpeed(float speed)
    {
        if (agent != null)
        {
            agent.speed = speed;
        }
    }
}