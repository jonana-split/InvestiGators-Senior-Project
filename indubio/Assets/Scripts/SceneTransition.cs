using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private string sceneName = "SceneName"; // Replace with your actual scene name
    [SerializeField] private float delay = 5f;

    private void Start()
    {
        Invoke(nameof(LoadScene), delay);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
