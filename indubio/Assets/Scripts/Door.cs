using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Door : MonoBehaviour
{
    public string targetScene; // Scene to load when entering this door
    public int doorNumber; // Identifier for this door
    [SerializeField] private Image fadeOverlay; // Reference to the UI Image for fading
    [SerializeField] private float fadeDuration = 1.0f;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.gameObject.GetComponent<player>().freeze();
            Debug.Log($"Entered door {doorNumber}, loading scene {targetScene}");
            //SceneManagerCustom.instance.LoadScene(targetScene, doorNumber);
            StartCoroutine(FadeOutAndLoadScene());
        }
    }

    private IEnumerator FadeOutAndLoadScene()
    {
        Color color = fadeOverlay.color;
        float time = 0f;

        fadeOverlay.gameObject.SetActive(true);

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, time / fadeDuration);
            fadeOverlay.color = color;
            yield return null;
        }

        // Ensure it's fully opaque
        color.a = 1f;
        fadeOverlay.color = color;

        SceneManagerCustom.instance.LoadScene(targetScene, doorNumber);
    }
}
