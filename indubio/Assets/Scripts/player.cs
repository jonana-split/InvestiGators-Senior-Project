using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using TMPro;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using Unity.Mathematics;

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
    TextMeshProUGUI speakToAshlyn;
    TextMeshProUGUI speakToOlive;
    TextMeshProUGUI speakToAlison;
    TextMeshProUGUI lookForClues;
    TextMeshProUGUI doorLockedTxt;

    private InvItem currentItem;
    private bool collectItem = false;

    public int npcVisit = 3;
    public static bool threeNPCs = false;
    public bool isDay1 = false;

    public GameObject currDoor;
    bool frozen = false;
    public GameObject npcTracker;
    CollapseInventory invManager;
    slotImg slot2;
    JournalManager journalManager;
    void Start()
    {
        var music = GameObject.FindWithTag("music");
        if (music != null)
        {
            Transform bgMusic = music.transform.Find("BackgroundMusic");
            
            if(bgMusic != null)
            {
                Debug.Log("found music");
                bgMusic.gameObject.SetActive(true);
            }
            
        }
        else
        {
            Debug.LogWarning("No bgMusic");
        }

        npcTracker = GameObject.FindWithTag("npcTracker");

        if (SceneManager.GetActiveScene().name == "MainRoom_Day1_MONOLOGUE")
        {
            if (isDay1)
            {
                isDay1 = false;
                threeNPCs = false;
            }

            Debug.Log("mainroom");

            Destroy(npcTracker);
            
            currDoor = GameObject.Find("Door4");

            if (currDoor != null)
            {
                Door door = currDoor.GetComponent<Door>();

                if (door)
                {
                    door.targetScene = "Bedroom4_Day1_2_BEFORE_COMBAT";
                }
            }
        }

        box = GetComponent<BoxCollider2D>();
        move = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody2D>();
        var inventoryUI = GameObject.FindWithTag("HUDDontDestroy");
        var hud = inventoryUI.transform.Find("InventoryOpenBtn").gameObject;
        inventory = GameObject.FindWithTag("Inventory").GetComponent<Inventory>();
        pressE = inventoryUI.transform.Find("PressE").gameObject.GetComponent<TextMeshProUGUI>();
        speakToAlison = inventoryUI.transform.Find("speakToAlison").gameObject.GetComponent<TextMeshProUGUI>();
        speakToAshlyn = inventoryUI.transform.Find("speakToAshlyn").gameObject.GetComponent<TextMeshProUGUI>();
        speakToOlive = inventoryUI.transform.Find("speakToOlive").gameObject.GetComponent<TextMeshProUGUI>();
        lookForClues = inventoryUI.transform.Find("lookForClues").gameObject.GetComponent<TextMeshProUGUI>();
        doorLockedTxt = inventoryUI.transform.Find("doorlocked").GetComponent<TextMeshProUGUI>();
        invManager = inventoryUI.transform.Find("InventoryManager").gameObject.GetComponent<CollapseInventory>();
        journalManager = GameObject.Find("JournalManager").GetComponent<JournalManager>();
        var slot1 = hud.transform.Find("Inventory");
        slot2 = null;
        if (slot1 != null)
        {
            slot1 = transform.Find("Slot");
        }
        if (slot1 != null)
        {
            slot2 = transform.Find("Border1").gameObject.GetComponent<slotImg>();

        }
        if (hud != null)
        {
            hud.SetActive(true);
            Debug.Log("Inv button back");
        }
        else
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
    public void freeze()
    {
        
        if (slot2 != null)
        {
            slot2.ClosePopup();
        }
        if (invManager != null)
        {
            invManager.CloseInventory();
            invManager.enabled=false;
        }
    
        if (journalManager != null)
        {
            //journalManager.CloseJournal();
            journalManager.enabled=false;
        }
        frozen = true;
        rb.linearVelocity=Vector2.zero;
        animator.SetBool("walking", false);
    }
    public void unfreeze()
    {
        frozen = false;
        if (invManager != null)
        {
            invManager.enabled = true;
        }
        if (journalManager != null)
        {
            journalManager.enabled = true;
        }
    }
    // Update is called once per frame
    void Update()
    {

        if (SceneManager.GetActiveScene().name == "Bedroom4_Day1_1_INITIAL" || SceneManager.GetActiveScene().name == "Bedroom2_Day1" || SceneManager.GetActiveScene().name == "Bedroom1_Day1")
        {
            currDoor = GameObject.FindWithTag("Door");

            if (threeNPCs == true)
            {
                if (currDoor != null)
                {
                    Door door = currDoor.GetComponent<Door>();

                    if (door && isDay1)
                    {
                        door.targetScene = "MainRoom_Day1_MONOLOGUE";
                        isDay1 = false;
                    }
                }

            }
        }

        if (Inventory.keyCol)
        {
            //why does this do nothing?
        }
        if (!frozen)
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
        }
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

            /*
             * we don't really need the press E anymore
             * 
             * if (GameObject.FindWithTag("key"))
            {
                pressE.gameObject.SetActive(true);
            }
            else if (GameObject.FindWithTag("knife"))
            {
                pressE.gameObject.SetActive(true);
            }
            */

        }

        if (collision.CompareTag("NPC"))
        {
            string npcName = collision.gameObject.name;

            if (!threeNPC_Tracker.allNpcNames.Contains(npcName))
            {
                threeNPC_Tracker.allNpcNames.Add(npcName);

                Debug.Log("NPC Count: " + threeNPC_Tracker.allNpcNames.Count);
                Debug.Log("NPC Name: " + npcName);
            }
        }

        if (threeNPC_Tracker.allNpcNames.Count == 3 && isDay1)
        {
            threeNPCs = true;
        }

        /*else if (collision.gameObject.tag == "NPC")
        {
            pressE.gameObject.SetActive(true);
        }*/


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<InvItem>() != null)
        {
            collectItem = false;
            pressE.gameObject.SetActive(false);
        }

        /*
        else if (collision.gameObject.tag == "NPC")
        {
            pressE.gameObject.SetActive(false);
        }*/


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "locked")
        {
            Debug.Log("Door collided");
            doorLockedTxt.gameObject.SetActive(true);
        }else if (collision.gameObject.tag == "cellarlocked" && !Inventory.keyCol)
        {
            doorLockedTxt.gameObject.SetActive(true);
        }
        else if (collision.gameObject.tag == "ashlynSpeak")
        {
            speakToAshlyn.gameObject.SetActive(true);
        }
        else if (collision.gameObject.tag == "oliveSpeak")
        {
            speakToOlive.gameObject.SetActive(true);
        }
        else if (collision.gameObject.tag == "alisonSpeak")
        {
            speakToAlison.gameObject.SetActive(true);
        }
        else if (collision.gameObject.tag == "lookForClues")
        {
            lookForClues.gameObject.SetActive(true);
        }

        if (Inventory.keyCol && collision.gameObject.tag == "cellarlocked")
        {
            collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "locked")
        {
            doorLockedTxt.gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "cellarlocked" && !Inventory.keyCol)
        {
            doorLockedTxt.gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "ashlynSpeak")
        {
            speakToAshlyn.gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "oliveSpeak")
        {
            speakToOlive.gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "alisonSpeak")
        {
            speakToAlison.gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "lookForClues")
        {
            lookForClues.gameObject.SetActive(false);
        }
    }

}