using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

//CITATIONS: Some reference to https://www.youtube.com/watch?v=-xB4xEmGtCY

public interface InvItem
{
    string Name { get; }
    
    Sprite Image { get;}

    void OnPickup();
}

public class InventoryEvents : EventArgs
{
    public InventoryEvents(InvItem item)
    {
        Item = item;
    }
    public InvItem Item;
}
