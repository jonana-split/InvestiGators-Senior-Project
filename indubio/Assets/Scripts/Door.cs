using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public string targetScene; // Scene to load when entering this door
    public int doorNumber; // Identifier for this door

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Debug.Log($"Entered door {doorNumber}, loading scene {targetScene}");
            SceneManagerCustom.instance.LoadScene(targetScene, doorNumber);
        }
    }
}
