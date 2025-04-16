using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public string SceneToLoad1;
    public string SceneToLoad2;
    public bool credits;

    public void SwitchSceneControl(bool creditsInput)
    {
        credits = creditsInput;
    }

    public void SwitchTheScene()
    {
        if (credits)
        {
            Debug.Log("credits true");
            SceneManager.LoadScene(SceneToLoad1);
        }
        else
        {
            Debug.Log("credits false");
            SceneManager.LoadScene(SceneToLoad2);
        }
    }
}
