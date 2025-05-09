using UnityEngine; // Import Unity engine functionalities
using UnityEngine.AI; // Import NavMeshAgent for pathfinding

public class PlayerChasing : MonoBehaviour // Class for enemy AI behavior
{
<<<<<<< Updated upstream
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
=======
    private Transform player; // Reference to the player's Transform
    private NavMeshAgent agent; // Reference to the NavMeshAgent component

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component attached to this GameObject
        player = GameObject.FindGameObjectWithTag("Player")?.transform; // Find the player GameObject by its "Player" tag and get its Transform
        if (player == null) // Check if the player was not found
>>>>>>> Stashed changes
        {
            Debug.LogWarning("Player not found! Make sure your player has the 'Player' tag."); // Log a warning if no player is found
        }
    }

    void Update()
    {
<<<<<<< Updated upstream
        // Only set the destination if the player is visible
        if (player != null && canSeePlayer)
=======
        if (player != null) // Check if the player reference is valid
>>>>>>> Stashed changes
        {
            agent.SetDestination(player.position); // Set the NavMeshAgent's destination to the player's position
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