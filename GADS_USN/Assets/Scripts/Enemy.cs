using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damage = 10; 

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthSystem playerHealth = collision.gameObject.GetComponent<HealthSystem>();

            if (collision.contacts[0].normal.y < -0.5)
            {
                
                Destroy(gameObject);
            }
            else
            {
                
                playerHealth.TakeDamage(damage);
            }
        }
    }
}