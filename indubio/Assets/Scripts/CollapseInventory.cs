using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CollapseInventory : MonoBehaviour
{
    public GameObject inventory;


    public void Start()
    {
        CanvasGroup inv = inventory.GetComponent<CanvasGroup>();
        inv.alpha = 0f;
        inv.interactable = false;
        inv.blocksRaycasts = false;
    }

    public void OpenInventory()
    {
        CanvasGroup inv = inventory.GetComponent<CanvasGroup>();

        inv.alpha = 1f;
        inv.interactable = true;
        inv.blocksRaycasts = true;

    }

    public void CloseInventory()
    {
        CanvasGroup inv = inventory.GetComponent<CanvasGroup>();
        inv.alpha = 0f;
        inv.interactable = false;
        inv.blocksRaycasts = false;
    }
}