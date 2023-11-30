using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject mainMenuCanvas; // Drag your main menu canvas here
    public GameObject optionsMenuCanvas; // Drag your options menu canvas here

    void Start()
    {
        // Ensure the main menu is visible and the options menu is hidden at the start
        mainMenuCanvas.SetActive(true);
        optionsMenuCanvas.SetActive(false);
    }

    public void ShowOptionsMenu()
    {
        // Hide the main menu and show the options menu
        mainMenuCanvas.SetActive(false);
        optionsMenuCanvas.SetActive(true);
    }

    public void ShowMainMenu()
    {
        // Hide the options menu and show the main menu
        mainMenuCanvas.SetActive(true);
        optionsMenuCanvas.SetActive(false);
    }
}