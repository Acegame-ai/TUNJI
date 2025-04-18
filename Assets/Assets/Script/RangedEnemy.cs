using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public float speed = 2f;
    public float detectionRange = 10f;
    public float attackRange = 6f;
    public float attackCooldown = 2f;
    public GameObject projectilePrefab;
    public Transform firePoint;

    private Transform player;
    private bool canAttack = true;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < attackRange && canAttack)
        {
            AttackPlayer();
        }
        else if (distance < detectionRange)
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
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().linearVelocity = (player.position - firePoint.position).normalized * 5f;

        // Attack cooldown
        Invoke(nameof(ResetAttack), attackCooldown);
    }

    void ResetAttack()
    {
        canAttack = true;
    }
}
