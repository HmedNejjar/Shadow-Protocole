using UnityEngine;
using UnityEngine.AI;

public class PlayerChasing : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
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
        if (player != null)
        {
            agent.SetDestination(player.position);
        }
    }
}
