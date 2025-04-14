using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class ShowItem : MonoBehaviour
{
    public GameObject itempopup;
    public Image image;
    public TextMeshProUGUI itemname;
    public TextMeshProUGUI description;


    public void OpenPopup(InvItem item)
    {
        itempopup.SetActive(true);
        itemname.text = item.Name;
        description.text = "Test";
        image.sprite = item.Image;
    }

    public void ClosePopup(InvItem item)
    {
        itempopup.SetActive(false);
    }
}