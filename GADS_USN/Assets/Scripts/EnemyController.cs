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
        // Move towards the target
        Vector3 direction = target - transform.position;
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Check if the object has not already reached the target
        if (direction != Vector3.zero)
        {
            // Negate the direction vector
            direction = -direction;

            // Calculate the target rotation
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Calculate the current rotation towards the camera
            Quaternion cameraRotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);

            // Calculate the final rotation as the combination of the target rotation and the camera rotation
            Quaternion finalRotation = Quaternion.Euler(cameraRotation.eulerAngles.x, targetRotation.eulerAngles.y, cameraRotation.eulerAngles.z);

            // Rotate the object towards the final rotation
            transform.rotation = Quaternion.Lerp(transform.rotation, finalRotation, speed * Time.deltaTime);
        }
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