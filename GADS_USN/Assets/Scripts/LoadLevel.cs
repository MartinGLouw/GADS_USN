using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Load the scene in list
            SceneManager.LoadScene("Level 1");
            
        }
    }
    public void LoadLevel1()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level Training");
    }
    void Start()
    {
        Debug.Log(Application.persistentDataPath);
    }
}
