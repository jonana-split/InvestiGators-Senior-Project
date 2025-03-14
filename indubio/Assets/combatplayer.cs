using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class combatplayer : MonoBehaviour
{
    private int hp = 100;
    private int damage = 10;
    private float shootCool = .25f;
    private float shootCount = 0;
    private float invinCool = 1f;
    private float invinCount = 0;
    bool invin = false;
    private BoxCollider2D box;
    private Vector2 moveDelta, aimdir;
    private InputAction move;
    private InputAction slow, shoot;
    private Rigidbody2D rb;
    private GameObject pivot;
    bool slowed = false;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float speed2 = 2.5f;
    [SerializeField] private GameObject combatBox, hurtbox, colBox;
    [SerializeField] private Slider slider;
    private Vector2 boundsMin, boundsMax, playerSize;
    private Camera cam;
    public GameObject bulletPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shootCount = shootCool;
        box = GetComponent<BoxCollider2D>();
        move = InputSystem.actions.FindAction("Move");
        slow = InputSystem.actions.FindAction("Slow");
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
        hurtbox.GetComponent<SpriteRenderer>().enabled = false;
    }
    void transparency(float a)
    {
        var color = GetComponent<SpriteRenderer>().material.color;
        color.a = a;
        GetComponent<SpriteRenderer>().material.color = color;
    }
    // Update is called once per frame
    void Update()
    {
        if(invin)
        {
            invinCount += Time.deltaTime;
            if(invinCount > invinCool)
            {
                invinCount = 0;
                invin = false;
                transparency(1);
                colBox.GetComponent<Collider2D>().enabled = true;
                hurtbox.GetComponent<BoxCollider2D>().enabled = true;
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
        if (moveDelta.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (moveDelta.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (slow.inProgress)
        {
            slowed = true;

            hurtbox.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            slowed = false;
            hurtbox.GetComponent<SpriteRenderer>().enabled = false;
        }
        Vector2 targ = new Vector2(transform.position.x, transform.position.y) + moveDelta;

        moveDelta *= (slowed) ? speed2 : speed;
        //Debug.Log(moveDelta);
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
            slider.value = hp;
            transparency(.5f);
            invin = true;
            colBox.GetComponent<Collider2D>().enabled = false;
            hurtbox.GetComponent<BoxCollider2D>().enabled = false;
            //Debug.Log(collider.gameObject.name);
            collider.gameObject.SendMessage("hitPlayer",SendMessageOptions.DontRequireReceiver);
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        hurtboxHit(collision.collider);
    }
}
