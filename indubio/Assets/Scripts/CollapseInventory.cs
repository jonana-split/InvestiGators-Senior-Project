using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

//CITATIONS: Some reference to https://www.youtube.com/watch?v=-xB4xEmGtCY

public class CollapseInventory : MonoBehaviour
{
    public GameObject inventory;
    public bool enabled = true;


    public void Start()
    {
        CanvasGroup inv = inventory.GetComponent<CanvasGroup>();
        inv.alpha = 0f;
        inv.interactable = false;
        inv.blocksRaycasts = false;
    }

    public void OpenInventory()
    {
        if (!enabled)
        {
            return;
        }
        CanvasGroup inv = inventory.GetComponent<CanvasGroup>();
        Debug.Log("Attempting to open inventory");
        inv.alpha = 1f;
        inv.interactable = true;
        inv.blocksRaycasts = true;

    }

    public void CloseInventory()
    {
        if (!enabled)
        {
            return;
        }
        CanvasGroup inv = inventory.GetComponent<CanvasGroup>();
        inv.alpha = 0f;
        inv.interactable = false;
        inv.blocksRaycasts = false;
    }
}