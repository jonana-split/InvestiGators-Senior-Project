using UnityEngine;

public class KeepItem: MonoBehaviour {
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
