using UnityEngine;
using UnityEngine.InputSystem;

public class player : MonoBehaviour
{
    private BoxCollider2D box;
    private Vector2 moveDelta;
    private InputAction move;
    private Rigidbody2D rb;
    [SerializeField] private float speed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        move = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDelta = move.ReadValue<Vector2>();
        Debug.Log(moveDelta);
        if (moveDelta.x > 0)
        {
            transform.localScale = Vector3.one;
        }else if (moveDelta.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        moveDelta *= speed;
        Debug.Log(moveDelta);
        rb.linearVelocity = moveDelta;
        Debug.Log(rb.linearVelocity);
    }
}
