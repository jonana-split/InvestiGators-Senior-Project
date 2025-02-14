using UnityEngine;
using UnityEngine.InputSystem;

public class player : MonoBehaviour
{
    private BoxCollider2D box;
    private Vector3 moveDelta;
    private InputAction move;
    private float speed = .001f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        move = InputSystem.actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        moveDelta = move.ReadValue<Vector2>();
        if(moveDelta.x > 0)
        {
            transform.localScale = Vector3.one;
        }else if (moveDelta.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        transform.Translate(moveDelta * speed);
    }
}
