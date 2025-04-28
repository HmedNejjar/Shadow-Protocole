using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [Header("Damage Settings")]
    public float damage = 30f;
    public float attackCooldown = 1f;
    public float attackRange = 1.5f;
    
    private float attackTimer;
    private HealthUpdate playerHealth;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player != null)
        {
            playerHealth = player.GetComponent<HealthUpdate>();
        }
    }

    void Update()
    {
        if (player == null || playerHealth == null || playerHealth.IsDead) 
            return;

        attackTimer += Time.deltaTime;

        if (Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            if (attackTimer >= attackCooldown)
            {
                playerHealth.TakeDamage(damage);
                attackTimer = 0f;
                Debug.Log($"Dealt {damage} damage to player");
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}