using UnityEngine;

public class MusicControl : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ToggleMusic(bool isOn)
    {
        if (isOn)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }
}