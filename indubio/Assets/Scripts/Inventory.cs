using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    private const int slots = 4;

    private List<InvItem> n = new List<InvItem>();

    public event EventHandler<InventoryEvents> newItemAdded;

    public List<slotImg> numSlots;

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

                numSlots[n.Count-1].currItem(item);

                if (newItemAdded != null)
                {
                    newItemAdded(this, new InventoryEvents(item));
                }
            }
        }
    }
}
