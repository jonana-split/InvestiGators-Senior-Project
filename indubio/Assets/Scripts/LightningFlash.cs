using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LightningFlash : MonoBehaviour
{
    public Image panelImage; // Reference to the panel's Image component
    public float flashDuration = 0.1f; // How long each flash lasts
    public float delayBetweenFlashes = 0.2f; // Time between two flashes
    public float delay = 1.0f;

    void Start()
    {
        // Optional: Start the flash automatically on start
        panelImage.enabled = false;
        StartCoroutine(FlashTwice());
    }

    public IEnumerator FlashTwice()
    {
        yield return new WaitForSeconds(delay);
        yield return StartCoroutine(FlashOnce());
        yield return new WaitForSeconds(delayBetweenFlashes);
        yield return StartCoroutine(FlashOnce());
    }

    private IEnumerator FlashOnce()
    {
        panelImage.enabled = true;
        yield return new WaitForSeconds(flashDuration);
        panelImage.enabled = false;
    }
}
