using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider slider;
    public float mouseSensitivity = 100f;
    public AudioSource plopVolumeTest;

    void Start()
    {
        mouseSensitivity = PlayerPrefs.GetFloat("currentSensitivity", 100);
        slider.value = mouseSensitivity / 10;
    }

    public void AdjustSpeed(float newSpeed)
    {
        mouseSensitivity = newSpeed * 10;
    }

    public void SetSensitivity(float sensitivity)
    {
        PlayerPrefs.SetFloat("sensitivity", sensitivity);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);  // https://gamedevbeginner.com/10-unity-audio-tips-that-you-wont-find-in-the-tutorials-2/#volume_sliders
        //audioMixer.SetFloat("volume", volume);
        if (!plopVolumeTest.isPlaying) {
            plopVolumeTest.Play();
        }
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}
