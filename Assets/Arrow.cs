using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int damage = 25;
    public float lifeTime = 3f;
    
    void Start()
    {
        Destroy(gameObject, lifeTime); // Destroy arrow after a few seconds
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Enemy"))
        {
            hitInfo.GetComponent<EnemyHealth>().TakeDamage(damage);
            Destroy(gameObject); // Destroy arrow on impact
        }
        else if (hitInfo.CompareTag("Wall"))
        {
            Destroy(gameObject); // Arrow disappears when it hits a wall
        }
    }
}
