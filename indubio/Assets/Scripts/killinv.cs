using UnityEngine;

public class killinv : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var music = GameObject.FindWithTag("music");
        if (music != null)
        {
            Destroy(music);
        }
        var npc = GameObject.FindWithTag("npcTracker");
        if (npc != null)
        {
            Destroy(npc);
        }
        var BGmusic = GameObject.FindWithTag("bgMusic");
        if (BGmusic != null)
        {
            Destroy(music);
        }
        if (npc != null)
        {
            Destroy(npc);
        }
        var hud = GameObject.FindWithTag("HUDDontDestroy");
        if (hud != null)
        {
            Destroy(hud);
        }
        var SceneManager = GameObject.Find("SceneManager");
        if(SceneManager != null)
        {
            Destroy(SceneManager);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
