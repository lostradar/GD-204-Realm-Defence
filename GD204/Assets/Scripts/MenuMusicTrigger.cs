using UnityEngine;

public class MenuMusicTrigger : MonoBehaviour
{
    void Start()
    {
        
        if (AudioManager.instance != null)
        {
            AudioManager.instance.ChangeMusic(AudioManager.instance.menuTheme);
        }
    }
}
