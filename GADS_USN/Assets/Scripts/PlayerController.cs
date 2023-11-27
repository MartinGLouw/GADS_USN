using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    public float stamina = 100f;
    public float staminaDrainRate = 20f;
    public float staminaCooldown = 1f; // Changed to 1 second
    public float pickupDuration = 5f; // Duration of the pickup effect
    public Slider staminaBar;

    private bool isJumping = false;
    private bool canDoubleJump = false;
    private float currentStamina;
    private Rigidbody rb;
    private bool hasPickup = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentStamina = stamina;
        staminaBar.maxValue = stamina;
        staminaBar.value = currentStamina;
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");

        rb.velocity = new Vector3(moveX * speed, rb.velocity.y, 0);

        if (Input.GetButtonDown("Jump") && !isJumping && currentStamina > 0)
        {
            Jump();
            isJumping = true;
            canDoubleJump = true;
            DrainStamina();
        }
        else if (Input.GetButtonDown("Jump") && isJumping && canDoubleJump && currentStamina > 0)
        {
            Jump();
            canDoubleJump = false;
            DrainStamina();
        }

        staminaBar.value = currentStamina;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
        else if (collision.gameObject.CompareTag("PickupTime"))
        {
            hasPickup = true;
            Destroy(collision.gameObject);
            StartCoroutine(PickupEffect());
        }
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, 0f);
        rb.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
    }

    void DrainStamina()
    {
        currentStamina -= staminaDrainRate;
        if (currentStamina < 0)
        {
            currentStamina = 0;
        }
        StartCoroutine(StaminaCooldown());
    }

    IEnumerator StaminaCooldown()
    {
        yield return new WaitForSeconds(hasPickup ? staminaCooldown / 2 : staminaCooldown);
        currentStamina = stamina;
    }

    IEnumerator PickupEffect()
    {
        yield return new WaitForSeconds(pickupDuration);
        hasPickup = false;
    }
}
