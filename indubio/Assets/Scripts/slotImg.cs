using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
public class slotImg: MonoBehaviour 
{
    private InvItem current;
    public Button button;

    public GameObject itempopup;
    public TextMeshProUGUI itemname;
    public TextMeshProUGUI description;

    public void currItem(InvItem item)
    {
        current = item;
    }

    public void OnClick()
    {
        if (current != null)
        {
            itempopup.SetActive(true);
            itemname.text = current.Name;
            if (itemname.text == "Knife"){
                description.text = "I might have found the murder weapon... but why is it here?";
            }else if(itemname.text == "Note")
            {
                description.text = "<i>'Dear Devin, please meet me outside your room tonight. I need to speak with you privately. From, - '</i> The sender's name is smudged." ;
            }
            else if (itemname.text == "Screwdriver")
            {
                description.text = "What could I unscrew with this?";
            }
            else if (itemname.text == "Key")
            {
                description.text = "I don't recall any locked doors... there must be a door I don't know about.";
            }

        }
    }

    public void ClosePopup()
    {
        itempopup.SetActive(false);
    }

}
