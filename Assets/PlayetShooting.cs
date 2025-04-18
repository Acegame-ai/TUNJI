using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform firePoint;
    public float arrowSpeed = 10f;
    public float fireRate = 0.5f; // Delay between shots

    private float nextFireTime = 0f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime) // Left-click to shoot
        {
            ShootArrow();
            nextFireTime = Time.time + fireRate;
        }
    }

    void ShootArrow()
    {
        // Get the mouse position in world space
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0; // Keep it in 2D

        // Calculate direction from fire point to the mouse position
        Vector2 shootDirection = (mousePos - firePoint.position).normalized;

        // Create the arrow
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        
        // Set velocity
        rb.linearVelocity = shootDirection * arrowSpeed;

        // Rotate the arrow to face the direction it's going
        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        arrow.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
