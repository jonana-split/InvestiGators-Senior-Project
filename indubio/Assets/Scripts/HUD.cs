using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HUD: MonoBehaviour
{
    public Inventory inventory;

    void Start()
    {
        inventory.newItemAdded += InventoryScript_newItemAdded;
    }

    private void InventoryScript_newItemAdded(object sender, InventoryEvents i)
    {
        Transform invPanel = transform.Find("Inventory");
        
        foreach (Transform slot in invPanel)
        {
            Image image = slot.GetChild(0).GetChild(0).GetComponent<Image>();

            if (!image.enabled)
            {
                image.enabled = true;
                image.sprite = i.Item.Image;
                break;
            }
        }
    }
}
