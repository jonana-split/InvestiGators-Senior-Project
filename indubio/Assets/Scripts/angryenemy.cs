using Unity.VisualScripting;
using UnityEngine;

public class angryenemy : MonoBehaviour
{
    private int hp = 2;
    private GameObject col;
    private Rigidbody2D rb;
    bool slowed = false;
    [SerializeField] private float speed = 1.6f;
    [SerializeField] private float lerpspeed = .5f;
    float moveCool = 2;
    float moveTime = 0;
    bool freeze = false;
    float freezeCool = .5f;
    GameObject player;
    Vector2 moveDir = Vector2.one.normalized;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //col = transform.Find("enemyColBox").gameObject;
        player = GameObject.Find("playercombat");
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
        
        if (freeze)
        {
            //Debug.Log("Frozen " + moveTime);
            moveTime += Time.deltaTime;

            if (moveTime >= freezeCool)
            {
                //Debug.Log("hi");
             
                freeze = false;
                moveTime = 0;
            }
        }
        else
        {
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, (player.transform.position - transform.position).normalized * speed, Time.deltaTime * lerpspeed);
        }

    }

    public void hurtboxHit(Collider2D collider)
    {
        Debug.Log("test" + collider.name);
        if (collider.gameObject.tag == "playerBullet")
        {
            Destroy(collider.gameObject);
            hp--;
            hitPlayer();
            if (hp <= 0)
            {
                Destroy(gameObject);
                combatmanager.enemyCount--;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //moveDir = Vector2.Reflect(moveDir, collision.GetContact(0).normal);
        //rb.linearVelocity = moveDir * speed;

    }
    public void hitPlayer()
    {
        //Debug.Log("here");
        freeze = true;
        rb.linearVelocity = Vector2.zero;
        moveTime = 0;

    }

}
