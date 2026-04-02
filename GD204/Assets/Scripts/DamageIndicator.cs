using UnityEngine;
using TMPro;
using System.Collections;

public class DamageIndicator : MonoBehaviour
{
    public TextMeshProUGUI textElement;
    private Canvas myCanvas;

    void Awake()
    {
        myCanvas = GetComponent<Canvas>();
        // Automatically finds the camera so you don't have to drag it in
        if (myCanvas != null) myCanvas.worldCamera = Camera.main;

        if (textElement != null) textElement.gameObject.SetActive(false);
    }

    public void ShowDamage(int amount)
    {
        if (textElement == null) return; // Prevents the error in your screenshot

        StopAllCoroutines();
        StartCoroutine(FlashText(amount));
    }

    IEnumerator FlashText(int amount)
    {
        textElement.text = amount.ToString();
        textElement.gameObject.SetActive(true);

        float elapsed = 0;
        float duration = 0.6f;
        Vector3 startPos = new Vector3(0, 0, 0); // Local center of canvas
        Vector3 endPos = new Vector3(0, 450f, 0); // Move up in "UI units"

        while (elapsed < duration)
        {
            textElement.transform.localPosition = Vector3.Lerp(startPos, endPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        textElement.gameObject.SetActive(false);
    }
}
