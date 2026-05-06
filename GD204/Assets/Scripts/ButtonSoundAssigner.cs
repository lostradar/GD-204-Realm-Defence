using UnityEngine;
using UnityEngine.UI;

public class ButtonSoundAssigner : MonoBehaviour
{
    void Start()
    {
        
        Button[] buttons = GetComponentsInChildren<Button>(true);

        foreach (Button btn in buttons)
        {
            
            btn.onClick.AddListener(() => {
                if (AudioManager.instance != null) AudioManager.instance.PlayButtonClick();
            });
        }
    }
}
