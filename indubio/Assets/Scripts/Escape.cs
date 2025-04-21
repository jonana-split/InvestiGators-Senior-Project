using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Escape : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            Debug.Log("Escape pressed - exiting play mode (Editor only)");
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
