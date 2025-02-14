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

    public GameObject bulletPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        move = InputSystem.actions.FindAction("Move");
        slow = InputSystem.actions.FindAction("Slow");
        shoot = InputSystem.actions.FindAction("Shoot");
        rb = GetComponent<Rigidbody2D>();
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
        }
        else
        {
            slowed = false;
        }
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
}
