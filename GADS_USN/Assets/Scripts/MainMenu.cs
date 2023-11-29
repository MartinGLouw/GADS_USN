using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            GameManager.Instance.score = 0;
            GameManager.Instance.playerName = null;
        }
    }

    public void OnPlayButtonClicked()
    {
        string playerName = nameInputField.text;
        string filePath = Path.Combine(Application.persistentDataPath, playerName + ".txt");

        int existingScore = 0;
        if (File.Exists(filePath))
        {
            string scoreTextFromFile = File.ReadAllText(filePath);
            int.TryParse(scoreTextFromFile, out existingScore);
        }

        GameManager.Instance.score = existingScore; // Set the score to existingScore
        GameManager.Instance.playerName = playerName;
        File.WriteAllText(filePath, GameManager.Instance.score.ToString());
    }


    public void OnShowScoreButtonClicked()
    {
        string playerName = nameInputField.text;
        string filePath = Path.Combine(Application.persistentDataPath, playerName + ".txt");

        if (File.Exists(filePath))
        {
            string scoreTextFromFile = File.ReadAllText(filePath);
            scoreText.text = "Score: " + scoreTextFromFile;
        }
        else
        {
            scoreText.text = "No score found for this player.";
        }
    }
}