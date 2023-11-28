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
    public Animator animator; // Reference to the Animator component

    private bool isJumping = false;
    private bool canDoubleJump = false;
    private float currentStamina;
    private Rigidbody rb;
    private bool hasPickup = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>(); // Get the Animator component from child objects
        currentStamina = stamina;
        staminaBar.maxValue = stamina;
        staminaBar.value = currentStamina;
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");

        rb.velocity = new Vector3(moveX * speed, rb.velocity.y, 0);

        // Rotate the player to face the direction of movement
        if (moveX > 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * speed);
        }
        else if (moveX < 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, -180, 0), Time.deltaTime * speed);
        }

        if (Input.GetButtonDown("Jump") && !isJumping && currentStamina > 0)
        {
            Jump();
            isJumping = true;
            canDoubleJump = true;
            DrainStamina();
            animator.SetTrigger("Jump"); // Set the Jump trigger
            animator.speed = 0.5f;
        }
        else if (Input.GetButtonDown("Jump") && isJumping && canDoubleJump && currentStamina > 0)
        {
            Jump();
            canDoubleJump = false;
            DrainStamina();
            animator.SetTrigger("Jump"); // Set the Jump trigger
            animator.speed = 0.5f;
        }



        staminaBar.value = currentStamina;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            animator.SetBool("Jump", false); // Set the Jump parameter to false
            animator.speed = 1f; // Reset the animation speed to normal
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
