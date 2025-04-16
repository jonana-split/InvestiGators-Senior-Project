using TMPro;
using UnityEngine;

public class ManageInv : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Inventory inventory;
    public TextMeshProUGUI pressE;
    public TextMeshProUGUI doorLockedTxt;

    private InvItem currentItem;
    private bool collectItem = false;

    private GameObject player;

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (collision.gameObject.tag == "locked")
        {
            Debug.Log("Door collided");
            doorLockedTxt.gameObject.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (collision.gameObject.tag == "locked")
        {
            doorLockedTxt.gameObject.SetActive(false);
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
}
