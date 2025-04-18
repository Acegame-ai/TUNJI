using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float sprintSpeed = 8f;
    private float currentSpeed;
    
    public int maxHealth = 100;
    public int currentHealth { get; private set; }

    public float maxStamina = 100f;
    public float currentStamina { get; private set; }
    public float staminaRegenRate = 10f;
    public float staminaDrainRate = 20f;

    public float maxMana = 100f;
    public float currentMana { get; private set; }
    public float manaRegenRate = 5f;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        currentMana = maxMana;
        currentSpeed = walkSpeed;
    }

    void Update()
    {
        // Movement Input
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize(); // Prevents diagonal speed boost

        // Sprinting Mechanic
        if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0)
        {
            currentSpeed = sprintSpeed;
            currentStamina -= staminaDrainRate * Time.deltaTime;
        }
        else
        {
            currentSpeed = walkSpeed;
            if (currentStamina < maxStamina)
            {
                currentStamina += staminaRegenRate * Time.deltaTime;
            }
        }

        // Regenerate Mana
        if (currentMana < maxMana)
        {
            currentMana += manaRegenRate * Time.deltaTime;
        }

        // Interaction
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * currentSpeed;
    }

    void Interact()
    {
        Debug.Log("Interacted!");
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Took damage! Health: " + currentHealth);
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    void Die()
    {
        Debug.Log("Player died!");
        // Add respawn logic here
    }
}
