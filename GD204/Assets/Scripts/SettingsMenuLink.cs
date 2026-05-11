using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuLink : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.LinkSliders(musicSlider, sfxSlider);
        }
    }
}
