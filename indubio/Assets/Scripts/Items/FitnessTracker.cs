using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FitnessTracker: MonoBehaviour, InvItem
{
    public string Name
    {
        get
        {
            return "Fitness Tracker";
        }
    }

    public Sprite _Image = null;

    public Sprite Image{

        get
        {
            return _Image;
        }
    
    }

    public void OnPickup()
    {
        gameObject.SetActive(false);
    }
}
