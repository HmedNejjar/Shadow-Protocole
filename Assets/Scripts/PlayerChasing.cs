using UnityEngine; // Import Unity engine functionalities
using UnityEngine.AI; // Import NavMeshAgent for pathfinding

public class PlayerChasing : MonoBehaviour // Class to handle enemy chasing the player
{
    private Transform player; // Reference to the player's Transform
    private NavMeshAgent agent; // Reference to the NavMeshAgent component

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component attached to this GameObject
        player = GameObject.FindGameObjectWithTag("Player")?.transform; // Find the player GameObject by its "Player" tag and get its Transform
        if (player == null) // Check if the player was not found
        {
            Debug.LogWarning("Player not found! Make sure your player has the 'Player' tag."); // Log a warning if no player is found
        }
    }

    void Update()
    {
        if (player != null) // Check if the player reference is valid
        {
            agent.SetDestination(player.position); // Set the NavMeshAgent's destination to the player's position
        }
    }
}
