using UnityEngine;

public class hurtbox : MonoBehaviour
{
    Collider2D col;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("TEST2");
        transform.parent.gameObject.SendMessage("hurtboxHit", collision);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("TEST");
        transform.parent.gameObject.SendMessage("hurtboxHit", collision.collider);

    }
    public void hitPlayer()
    {
        transform.parent.gameObject.SendMessage("hitPlayer", SendMessageOptions.DontRequireReceiver);
    }
}
