using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

//CITATIONS: Some reference to https://www.youtube.com/watch?v=-xB4xEmGtCY

public class Inventory : MonoBehaviour
{
    private const int slots = 4;

    private List<InvItem> n = new List<InvItem>();

    public event EventHandler<InventoryEvents> newItemAdded;

    public List<slotImg> numSlots;

    public static bool keyCol = false, gameCol = false, fitCol = false, knifeCol = false;

    public void AddItem(InvItem item)
    {
        Debug.Log("Adding item now");

        if (n.Count < slots)
        {
            Collider2D collider = (item as MonoBehaviour)?.GetComponent<Collider2D>();

            Debug.Log("count < slots");

            if (collider!= null && collider.enabled)
            {
                Debug.Log("Collision");
                collider.enabled = false;
                n.Add(item);
                
                item.OnPickup();

                if(item.Name == "Key")
                {
                    keyCol = true;
                }else if(item.Name == "Knife")
                {
                    knifeCol = true;
                }else if(item.Name == "Game Console")
                {
                    gameCol = true;
                }else if(item.Name == "Fitness Tracker")
                {
                    fitCol = true;
                }

                numSlots[n.Count-1].currItem(item);

                if (newItemAdded != null)
                {
                    newItemAdded(this, new InventoryEvents(item));
                }
            }
        }
    }
}
