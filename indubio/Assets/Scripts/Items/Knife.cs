using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Knife: MonoBehaviour, InvItem
{
    public string Name
    {
        get
        {
            return "Knife";
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
