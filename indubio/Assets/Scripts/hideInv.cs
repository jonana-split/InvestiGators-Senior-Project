using UnityEngine;

public class hideInv: MonoBehaviour
{
    void Start()
    {
        var hud = GameObject.FindWithTag("HUDDontDestroy").transform.Find("InventoryOpenBtn").gameObject;
        if (hud != null)
        {
            Debug.Log("feobfebiofewobef");
            hud.SetActive(false);
        }
    }
    
}
