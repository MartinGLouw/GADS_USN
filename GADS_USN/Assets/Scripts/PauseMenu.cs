using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool pauseState = false;

    public void Pause()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true); // Activate the canvas
        pauseState = true;
    }

    public void Unpause()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false); // Deactivate the canvas
        pauseState = false;
    }
}