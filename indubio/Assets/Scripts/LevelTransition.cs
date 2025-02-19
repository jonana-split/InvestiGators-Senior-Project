using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    public string SceneToLoad;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entered Trigger");
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("PLAYER ENTERED TRIGGER");
            SceneManager.LoadScene(SceneToLoad);
        }
    }
}
