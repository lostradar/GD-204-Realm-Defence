using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;

    public AudioClip buttonClickSound;
    public AudioClip menuTheme;
    public AudioClip gameOverTheme;
    public AudioClip[] levelTracks;

    private const string MusicSaveKey = "MusicVolume";
    private const string SFXSaveKey = "SFXVolume";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            
            LoadVolumeSettings();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Hub" || scene.name == "Start")
        {
            ChangeMusic(menuTheme);
        }
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Start")
        {
            ChangeMusic(menuTheme);
        }
    }


    public void SetMusicVolume(float value)
    {
        musicSource.volume = value;
        PlayerPrefs.SetFloat(MusicSaveKey, value);
    }

    public void SetSFXVolume(float value)
    {
        sfxSource.volume = value;
        PlayerPrefs.SetFloat(SFXSaveKey, value);
    }

    private void LoadVolumeSettings()
    {

        float musicVol = PlayerPrefs.GetFloat(MusicSaveKey, 0.5f);
        float sfxVol = PlayerPrefs.GetFloat(SFXSaveKey, 0.5f);

        musicSource.volume = musicVol;
        sfxSource.volume = sfxVol;
    }

    public void LinkSliders(Slider musicSlider, Slider sfxSlider)
    {

        musicSlider.value = musicSource.volume;
        sfxSlider.value = sfxSource.volume;


        musicSlider.onValueChanged.RemoveAllListeners(); 
        musicSlider.onValueChanged.AddListener(SetMusicVolume);

        sfxSlider.onValueChanged.RemoveAllListeners();
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }



    public void PlayMusicByIndex(int levelIndex)
    {
        if (levelTracks != null && levelIndex >= 0 && levelIndex < levelTracks.Length)
        {
            ChangeMusic(levelTracks[levelIndex]);
        }
        else
        {
            Debug.LogWarning("AudioManager: Index " + levelIndex + " not found in levelTracks array!");
        }
    }

    public void PlayGameOverMusic()
    {
        if (gameOverTheme != null)
        {
            ChangeMusic(gameOverTheme);
        }
    }

    public void ChangeMusic(AudioClip newClip)
    {
        if (newClip == null) return;
        if (musicSource.clip == newClip && musicSource.isPlaying) return;

        musicSource.Stop();
        musicSource.clip = newClip;
        musicSource.Play();
    }

    public void PlayButtonClick()
    {
        if (buttonClickSound != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(buttonClickSound, 0.25f);
        }
    }
}
