using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform waypoint1; 
    public Transform waypoint2; 
    public float speed = 2f; 

    private Vector3 target; 

    void Start()
    {
        target = waypoint1.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Waypoint"))
        {
            if (target == waypoint1.position)
            {
                target = waypoint2.position;
            }
            else if (target == waypoint2.position)
            {
                target = waypoint1.position;
            }
        }
    }
}