using Unity.VisualScripting;
//using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class combatplayer : MonoBehaviour
{
    public bool freeze=false;
    private int hp = 100;
    private int damage = 1;
    private float shootCool = .25f;
    private float shootCount = 0;
    private float invinCool = 1f;
    private float invinCount = 0;
    bool invin = false;
    public Animator animator;
    private BoxCollider2D box;
    private Vector2 moveDelta, aimdir;
    private InputAction move;
    private InputAction slow, shoot;
    private Rigidbody2D rb;
    public GameObject pivot;
    bool slowed = false;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float speed2 = 2.5f;
    [SerializeField] private GameObject combatBox, colBox;
    [SerializeField] private UnityEngine.UI.Slider slider;
    public combatmanager manager;

    private Vector2 boundsMin, boundsMax, playerSize;
    private Camera cam;
    public GameObject bulletPrefab;
    bool isWalking = false;
    Color norm =new Color(1.0f, 1.0f, 1.0f, 1.0f);
    Color ouch = new Color(1.0f, .5f, .5f, .5f);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("walking", false);
        shootCount = shootCool;
        box = GetComponent<BoxCollider2D>();
        move = InputSystem.actions.FindAction("Move");
        shoot = InputSystem.actions.FindAction("Shoot");
        rb = GetComponent<Rigidbody2D>();
        pivot = transform.Find("pivot").gameObject;
        cam = Camera.main;

        Vector3 min = combatBox.GetComponent<SpriteRenderer>().bounds.min;
        Vector3 max = combatBox.GetComponent<SpriteRenderer>().bounds.max;

        Vector3 screenMin = cam.WorldToScreenPoint(min);
        Vector3 screenMax = cam.WorldToScreenPoint(max);

        boundsMin = new Vector2 (screenMin.x, screenMin.y);
        boundsMax = new Vector2(screenMax.x, screenMax.y);
        Vector3 min2 = GetComponent<SpriteRenderer>().bounds.min;
        Vector3 max2 = GetComponent<SpriteRenderer>().bounds.max;

        Vector3 screenMin2 = cam.WorldToScreenPoint(min);
        Vector3 screenMax2 = cam.WorldToScreenPoint(max);
        playerSize = screenMax2- screenMin2;
        var hud = GameObject.FindWithTag("HUDDontDestroy").transform.Find("InventoryOpenBtn").gameObject;
        var music = GameObject.FindWithTag("bgMusic");
        if (hud != null)
        {
            Debug.Log("feobfebiofewobef");
            hud.SetActive(false);
            music.SetActive(false);
        }
    }
    void transparency(Color c)
    {
        GetComponent<SpriteRenderer>().material.color = c;
    }
    // Update is called once per frame
    public void unfreeze()
    {
        freeze = false;
    }
    public void resetForWave()
    {
        if (animator != null)
        {
            animator.SetFloat("x", 0);
            animator.SetFloat("y", 0);
            animator.SetBool("walking", false);
        }
        freeze = true;
        invinCount = 0;
        invin = false;
        colBox.GetComponent<Collider2D>().enabled = true;
        transparency(norm);
        transform.position = Vector2.zero;
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
    void Update()
    {
        if(freeze)
        {
            return;
        }
        if(invin)
        {
            invinCount += Time.deltaTime;
            if(invinCount > invinCool)
            {
                invinCount = 0;
                invin = false;
                transparency(norm);
                colBox.GetComponent<Collider2D>().enabled = true;
            }
        }
        if(shootCount<shootCool)
        {
            shootCount += Time.deltaTime;
        }
        var dir = cam.ScreenToWorldPoint(Input.mousePosition)-transform.position;

        dir.z = 0;
        dir = dir.normalized;

        pivot.transform.rotation = Quaternion.Euler(0, 0,Quaternion.LookRotation(Vector3.forward, dir).eulerAngles.z);
        aimdir = dir;
        moveDelta = move.ReadValue<Vector2>();
        //Debug.Log(moveDelta);
        
        Vector2 targ = new Vector2(transform.position.x, transform.position.y) + moveDelta;

        moveDelta *= (slowed) ? speed2 : speed;
        //Debug.Log(moveDelta);
        if(moveDelta.magnitude > 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

        //moveDelta *= speed;
        //Debug.Log(moveDelta);
        rb.linearVelocity = moveDelta;
        //Debug.Log(rb.linearVelocity);

        if (isWalking)
        {
            animator.SetFloat("x", moveDelta.x);
            animator.SetFloat("y", moveDelta.y);

        }
        animator.SetBool("walking", isWalking);

        rb.linearVelocity = moveDelta;
        //Debug.Log(rb.linearVelocity);
    }
    public void OnShoot(InputAction.CallbackContext ctx)
    {
        if (ctx.performed == true && shootCount>=shootCool && aimdir!=Vector2.zero)
        {
            shootCount = 0;
            var tmpBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            tmpBullet.GetComponent<bullet>().dir = aimdir;
        }

    }
    public void hurtboxHit(Collider2D collider)
    {
        if(collider.gameObject.tag=="damages" && !invin)
        {
            hp -= damage;
            if (hp <= 0)
            {
                animator.SetFloat("x", 0);
                animator.SetFloat("y", 0);
                animator.SetBool("walking", false);
                manager.gameOver();
            }
            else
            {
                slider.value = hp;
                transparency(ouch);
                invin = true;
                colBox.GetComponent<Collider2D>().enabled = false;
                //Debug.Log(collider.gameObject.name);
                collider.gameObject.SendMessage("hitPlayer", SendMessageOptions.DontRequireReceiver);
            }
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        hurtboxHit(collision.collider);
    }


}
