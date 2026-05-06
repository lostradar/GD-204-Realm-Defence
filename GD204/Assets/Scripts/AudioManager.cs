using UnityEngine;
using UnityEngine.SceneManagement; // Added for automatic scene detection

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    
    public AudioSource musicSource;
    public AudioClip buttonClickSound;
    public AudioSource sfxSource;

    
    public AudioClip menuTheme;
    public AudioClip gameOverTheme;

    
    public AudioClip[] levelTracks;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
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
        
        if (scene.name == "Hub")
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
            
            sfxSource.PlayOneShot(buttonClickSound);
        }
    }
}
