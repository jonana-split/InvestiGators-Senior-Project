using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

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
        Debug.Log(moveDelta);
        rb.linearVelocity = moveDelta;
        Debug.Log(rb.linearVelocity);

        if (isWalking)
        {
            animator.SetFloat("x", horizontal);
            animator.SetFloat("y", vertical);

        }

        animator.SetBool("walking", isWalking);

    }

}