using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public int meleeDamage = 25;
    public float attackRange = 1.5f;
    public float attackCooldown = 0.8f;
    public Transform attackPoint;
    public LayerMask enemyLayers;

    private bool canAttack = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && canAttack) // Press 'F' to attack
        {
            MeleeAttack();
        }
    }

    void MeleeAttack()
    {
        canAttack = false;
        Debug.Log("Player Melee Attack!");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(meleeDamage);
        }

        Invoke(nameof(ResetAttack), attackCooldown);
    }

    void ResetAttack()
    {
        canAttack = true;
    }

    void OnDrawGizmosSelected() // To visualize attack range in Unity
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
