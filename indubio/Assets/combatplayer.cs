using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class combatplayer : MonoBehaviour
{
    private BoxCollider2D box;
    private Vector2 moveDelta;
    private InputAction move;
    private InputAction slow, shoot;
    private Rigidbody2D rb;
    bool slowed = false;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float speed2 = 2.5f;
    [SerializeField] private GameObject combatBox, hurtbox;
    private Vector2 boundsMin, boundsMax, playerSize;

    public GameObject bulletPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        move = InputSystem.actions.FindAction("Move");
        slow = InputSystem.actions.FindAction("Slow");
        shoot = InputSystem.actions.FindAction("Shoot");
        rb = GetComponent<Rigidbody2D>();
        Camera cam = Camera.main;

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

    // Update is called once per frame
    void Update()
    {
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

        if (ctx.performed == true)
        {
            Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        }

    }
    public void hurtboxHit(Collider2D collider)
    {
        Debug.Log(collider.gameObject.name);
    }
}
