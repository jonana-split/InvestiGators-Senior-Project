using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.Timeline;

//CITATIONS: Some reference to https://www.youtube.com/watch?v=-xB4xEmGtCY
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
                description.text = "A knife found in a barrel in the wine cellar. There seems to be some blood on the blade";
            }else if(itemname.text == "Fitness Tracker"){
                description.text = "Ashlyn's fitness tracker that she provided as evidence. It seems to prove she was asleep during the murder due to its sleep tracking feature.";
            }
            else if (itemname.text == "Game Console"){
                description.text = "Olive's portable gaming console that she provided as evidence. It has time logs that seem to prove she was playing games during the murder";
            }else if (itemname.text == "Key"){
                description.text = "A key found in a hidden shelf in a kitchen cabinet. It must open something in the house";
            }

        }
    }

    public void ClosePopup()
    {
        itempopup.SetActive(false);
    }

}
