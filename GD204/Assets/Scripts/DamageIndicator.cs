using UnityEngine;
using TMPro;
using System.Collections;

public class DamageIndicator : MonoBehaviour
{
    public TextMeshProUGUI textElement;
    private Canvas myCanvas;

    private Coroutine statusCoroutine;

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

        //StopAllCoroutines();
        StartCoroutine(FlashText(amount));
    }

    public void ShowStatus(string statusName)
    {
        if (textElement == null) return;

        if (statusCoroutine != null) StopCoroutine(statusCoroutine);

        //StopAllCoroutines();
        // We pass the string directly to the Coroutine
        StartCoroutine(FlashStatusText(statusName));
    }

    IEnumerator FlashStatusText(string statusName)
    {
        textElement.text = statusName; // Use the word instead of a number
        textElement.gameObject.SetActive(true);

        float elapsed = 0;
        float duration = 1.5f; // Status text stays slightly longer
        Vector3 startPos = Vector3.zero;
        Vector3 endPos = new Vector3(0, 350f, 0);

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            t = t * t * (3f - 2f * t);
            textElement.transform.localPosition = Vector3.Lerp(startPos, endPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        textElement.gameObject.SetActive(false);
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
