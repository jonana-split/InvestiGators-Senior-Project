using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerCustom : MonoBehaviour
{
    public static SceneManagerCustom instance { get; private set; }

    private GameObject player;
    private GameObject[] doors;
    public int lastDoorNumber = -1; // Default to -1 if no previous door

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        doors = GameObject.FindGameObjectsWithTag("Door");

        if (scene.name == "MainRoom" && lastDoorNumber != -1)
        {
            // Find the door the player came from and spawn them next to it
            foreach (GameObject door in doors)
            {
                Door doorScript = door.GetComponent<Door>();
                if (doorScript != null && doorScript.doorNumber == lastDoorNumber)
                {
                    player.transform.position = door.transform.position + Vector3.down; // Offset so they don't instantly re-trigger
                    break;
                }
            }
        }
    }

    public void LoadScene(string sceneName, int doorFromNumber)
    {
        lastDoorNumber = doorFromNumber;
        SceneManager.LoadScene(sceneName);
    }
}
