using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] AudioMixer masterMixer;
    [SerializeField] Slider masterVolumeSlider;
    [SerializeField] float savedMasterVolume = 100f;

    private void Start()
    {
        masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", savedMasterVolume);
    }

    public void SetMasterVolume(float _value)
    {
        PlayerPrefs.SetFloat("Volume", _value);
        masterMixer.SetFloat("Volume", Mathf.Log10(_value) * 20);
    }
}
