using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using TMPro;

public class player : MonoBehaviour
{
    private BoxCollider2D box;
    private Vector2 moveDelta;
    private InputAction move;
    private Rigidbody2D rb;
    [SerializeField] private float speed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private bool isWalking;
    public Animator animator;
    private float horizontal;
    private float vertical;
    private Vector2 direction;

    Inventory inventory;
    TextMeshProUGUI pressE;
    TextMeshProUGUI doorLockedTxt;

    private InvItem currentItem;
    private bool collectItem = false;

    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        move = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody2D>();
        var inventoryUI = GameObject.FindWithTag("HUDDontDestroy");
        var hud = inventoryUI.transform.Find("InventoryOpenBtn").gameObject;
        inventory = GameObject.FindWithTag("Inventory").GetComponent<Inventory>();
        pressE = inventoryUI.transform.Find("PressE").gameObject.GetComponent<TextMeshProUGUI>();
        doorLockedTxt = inventoryUI.transform.Find("doorlocked").GetComponent<TextMeshProUGUI>();
        if (hud != null)
        {
            hud.SetActive(true);
            //Debug.Log("EOJefbouef");
        }else
        {
            //Debug.Log("dsffdfe");
        }

        if (Inventory.keyCol)
        {
            if (GameObject.FindWithTag("key"))
            {
                GameObject.FindWithTag("key").SetActive(false);
            }
        }
        if (Inventory.knifeCol)
        {
            if (GameObject.FindWithTag("knife"))
            {
                GameObject.FindWithTag("knife").SetActive(false);
            }
        }
        if (Inventory.fitCol)
        {
            if (GameObject.FindWithTag("fitness"))
            {
                GameObject.FindWithTag("fitness").SetActive(false);
            }
        }
        if (Inventory.gameCol)
        {
            if (GameObject.FindWithTag("game"))
            {
                GameObject.FindWithTag("game").SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        moveDelta = move.ReadValue<Vector2>();
        //Debug.Log(moveDelta);

        horizontal = moveDelta.x;
        vertical = moveDelta.y;

        if (horizontal != 0 || vertical != 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

        moveDelta *= speed;
        //Debug.Log(moveDelta);
        rb.linearVelocity = moveDelta;
        //Debug.Log(rb.linearVelocity);

        if (isWalking)
        {
            animator.SetFloat("x", horizontal);
            animator.SetFloat("y", vertical);

        }

        animator.SetBool("walking", isWalking);

        if (Input.GetKeyDown(KeyCode.E) && collectItem == true)
        {
            inventory.AddItem(currentItem);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        InvItem item = collision.GetComponent<InvItem>();
        if (item != null)
        {
            Debug.Log("Adding item");
            currentItem = item;
            collectItem = true;
            pressE.gameObject.SetActive(true);
            
        }else if (collision.gameObject.tag == "NPC")
        {
            pressE.gameObject.SetActive(true);
        }

        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.GetComponent<InvItem>() != null)
        {
            collectItem = false;
            pressE.gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "NPC")
        {
            pressE.gameObject.SetActive(false);
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "locked")
        {
            Debug.Log("Door collided");
            doorLockedTxt.gameObject.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "locked")
        {
            doorLockedTxt.gameObject.SetActive(false);
        }
    }

}