using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public int baseMaxHealth = 100; // The player's base maximum health
    public int currentHealth; 
    public int tempMaxHealth; // The player's temporary maximum health
    public Slider healthBar; 
    public bool hasProteinPowder = false; // Whether the player has picked up the protein powder
    private bool isInvulnerable = false; // Whether the player is currently invulnerable
    public bool isOverHealed = false; // Whether the player is currently over-healed

    void Start()
    {
        currentHealth = baseMaxHealth;
        tempMaxHealth = baseMaxHealth;
        healthBar.maxValue = baseMaxHealth;
        healthBar.value = currentHealth;
    }

    public void TakeDamage(int damage)
    {
        // If the player has the protein powder or is invulnerable, they don't take damage
        if (hasProteinPowder || isInvulnerable)
        {
            // Switch back to the normal model
            GetComponent<ProteinPickup>().SwitchToNormalModel();

            // Remove the protein powder effect
            hasProteinPowder = false;

            // Start the invulnerability period
            StartCoroutine(InvulnerabilityPeriod());
        }
        else
        {
            currentHealth -= damage; 

            // Ensure current health doesn't fall below 0
            if (currentHealth < 0)
            {
                currentHealth = 0;
            }
        }

        // Only update the health bar if the player is not over-healed
        if (!isOverHealed)
        {
            healthBar.value = currentHealth; 
        }
    }


    IEnumerator InvulnerabilityPeriod()
    {
        // Make the player invulnerable
        isInvulnerable = true;

        // Wait for 2 seconds
        yield return new WaitForSeconds(2);

        // Make the player vulnerable again
        isInvulnerable = false;
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount; 

        // Ensure current health doesn't exceed max health
        if (currentHealth > tempMaxHealth)
        {
            currentHealth = tempMaxHealth;
        }

        // Only update the health bar if the player is not over-healed
        if (!isOverHealed)
        {
            healthBar.value = currentHealth; 
        }
    }


    public void OverHeal(int extraHealth, float duration)
    {
        // Increase the player's maximum health
        tempMaxHealth += extraHealth;

        // Heal the player
        Heal(extraHealth);

        // Start the over-heal effect
        StartCoroutine(OverHealEffect(duration));
    }

    IEnumerator OverHealEffect(float duration)
    {
        // Set isOverHealed to true
        isOverHealed = true;

        // Wait for the duration of the over-heal effect
        yield return new WaitForSeconds(duration);

        // Reset the player's maximum health to the base maximum health
        tempMaxHealth = baseMaxHealth;

        // If the player's current health is greater than the base maximum health, reduce it
        if (currentHealth > baseMaxHealth)
        {
            currentHealth = baseMaxHealth;
        }

        // Update the health bar
        healthBar.value = currentHealth;

        // Set isOverHealed to false
        isOverHealed = false;
    }

}
