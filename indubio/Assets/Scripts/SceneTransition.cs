using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private string sceneName = "SceneName"; // Replace with your actual scene name
    [SerializeField] private float delay = 5f;
    [SerializeField] private Image fadeOverlay; // Reference to the UI Image for fading
    [SerializeField] private float fadeDuration = 1.0f;

    private void Start()
    {
        Invoke(nameof(StartFadeOut), delay);
    }

    private void StartFadeOut()
    {
        if (fadeOverlay != null)
        {
            StartCoroutine(FadeOutAndLoadScene());
        }
        else
        {
            Debug.LogWarning("FadeOverlay not assigned!");
            LoadScene(); // fallback
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

        LoadScene();
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
