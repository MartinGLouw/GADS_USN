using UnityEngine;
using TMPro;

public class PointsPopup : MonoBehaviour
{
    public GameObject Canvas;
    public TextMeshProUGUI Score;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Canvas.SetActive(true);
            // Add 10 points to the player's score
            GameManager.Instance.score += 10;

            // Destroy the points popup
            Destroy(gameObject);
        }
        //print to ui
        Score.text = "Score: " + GameManager.Instance.score;
    }
}