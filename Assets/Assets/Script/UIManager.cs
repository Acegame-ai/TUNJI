using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider healthBar;
    public Slider staminaBar;
    public Slider manaBar;

    private PlayerController player;

    void Start()
    {
        player = FindObjectOfType<PlayerController>(); // Find the player in the scene

        // Set max values
        healthBar.maxValue = player.maxHealth;
        staminaBar.maxValue = player.maxStamina;
        manaBar.maxValue = player.maxMana;
    }

    void Update()
    {
        // Update sliders to match player stats
        healthBar.value = player.currentHealth;
        staminaBar.value = player.currentStamina;
        manaBar.value = player.currentMana;
    }
}
