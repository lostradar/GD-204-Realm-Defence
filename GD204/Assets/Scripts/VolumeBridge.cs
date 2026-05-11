using UnityEngine;
using UnityEngine.UI;

public class VolumeBridge : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {

        if (AudioManager.instance != null)
        {

            musicSlider.value = AudioManager.instance.musicSource.volume;
            sfxSlider.value = AudioManager.instance.sfxSource.volume;

            musicSlider.onValueChanged.AddListener(AudioManager.instance.SetMusicVolume);
            sfxSlider.onValueChanged.AddListener(AudioManager.instance.SetSFXVolume);
        }
        else
        {
            Debug.LogError("VolumeBridge: Could not find AudioManager! Is it in the first scene?");
        }
    }

    private void OnDestroy()
    {

        if (AudioManager.instance != null)
        {
            musicSlider.onValueChanged.RemoveListener(AudioManager.instance.SetMusicVolume);
            sfxSlider.onValueChanged.RemoveListener(AudioManager.instance.SetSFXVolume);
        }
    }
}
