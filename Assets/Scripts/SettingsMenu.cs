using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume", 0);
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume", 0);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("musicVolume", volume);
        PlayerPrefs.SetFloat("musicVolume", volume);
        PlayerPrefs.Save();
    }

    public void SetSoundVolume(float volume)
    {
        audioMixer.SetFloat("sfxVolume", volume);
        PlayerPrefs.SetFloat("sfxVolume", volume);
        PlayerPrefs.Save();
    }

    public void Mute()
    {
        audioMixer.SetFloat("masterVolume", -80);
    }
}
