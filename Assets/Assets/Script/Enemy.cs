using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public float detectionRange = 5f;
    public float attackRange = 1.5f; // Attack when this close
    public int damage = 10;
    public float attackCooldown = 1.5f; // Time between attacks

    private Transform player;
    private bool isChasing = false;
    private bool canAttack = true;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < detectionRange && distance > attackRange)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }

        if (distance < attackRange && canAttack)
        {
            AttackPlayer();
        }

        if (isChasing)
        {
            ChasePlayer();
        }
    }

    void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    void AttackPlayer()
    {
        canAttack = false;
        Debug.Log("Enemy Attacked!");

        // Reduce player's health (we’ll add this in Player script)
        player.GetComponent<PlayerController>().TakeDamage(damage);

        // Attack cooldown
        Invoke(nameof(ResetAttack), attackCooldown);
    }

    void ResetAttack()
    {
        canAttack = true;
    }
}
