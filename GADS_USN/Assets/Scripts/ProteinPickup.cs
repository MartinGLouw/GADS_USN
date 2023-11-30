using System.Collections;
using UnityEngine;

public class ProteinPickup : MonoBehaviour
{
    public GameObject normalModel; // The normal player model
    public GameObject poweredModel; // The powered-up player model
    private HealthSystem healthSystem; // Reference to the HealthSystem script
    public int PickupDuration;
    public AudioClip pickupSound; // Drag your pickup sound here in the inspector
    public AudioSource audioSource;
    private AudioSource pickupAudioSource; // Separate AudioSource for the pickup sound

    void Start()
    {
        // Ensure the normal model is active and the powered model is inactive at the start
        normalModel.SetActive(true);
        poweredModel.SetActive(false);

        // Get the HealthSystem component
        healthSystem = GetComponent<HealthSystem>();

        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();

        // Create a new AudioSource for the pickup sound
        pickupAudioSource = gameObject.AddComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the player has collided with a protein powder pickup
        if (collision.gameObject.CompareTag("ProteinPowder"))
        {
            // Destroy the pickup
            Destroy(collision.gameObject);

            // Play the pickup sound
            pickupAudioSource.clip = pickupSound;
            pickupAudioSource.Play();

            // Switch to the powered model
            SwitchToPoweredModel();

            // Increase the player's health
            healthSystem.Heal(1);

            // Set hasProteinPowder to true
            healthSystem.hasProteinPowder = true;

            // Start a coroutine to switch back to the normal model after a certain amount of time
            StartCoroutine(SwitchBackAfterTime(PickupDuration));
        }
        // Check if the player has collided with a creatine pickup
        else if (collision.gameObject.CompareTag("Creatine"))
        {
            // Destroy the pickup
            Destroy(collision.gameObject);

            // Play the pickup sound
            pickupAudioSource.clip = pickupSound;
            pickupAudioSource.Play();

            // Over-heal the player
            healthSystem.OverHeal(50, PickupDuration);
        }
    }

    IEnumerator SwitchBackAfterTime(float time)
    {
        // Wait for the specified amount of time
        yield return new WaitForSeconds(time);

        // Switch back to the normal model
        SwitchToNormalModel();
    }

    public void TakeDamage(int damage)
    {
        // Call the TakeDamage method in the HealthSystem script
        healthSystem.TakeDamage(damage);

        // If the player's health drops to 1, switch back to the normal model
        if (healthSystem.currentHealth == 1)
        {
            SwitchToNormalModel();
        }
    }

    public void SwitchToNormalModel()
    {
        normalModel.SetActive(true);
        poweredModel.SetActive(false);
    }

    public void SwitchToPoweredModel()
    {
        normalModel.SetActive(false);
        poweredModel.SetActive(true);
    }
}
