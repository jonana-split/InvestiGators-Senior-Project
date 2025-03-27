using UnityEngine;

public class bullet : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private int speed = 5;
     public Vector2 dir = new Vector2(0,1);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dir = dir.normalized;
        rb.linearVelocity = speed*dir;
        Destroy(gameObject, 3);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
