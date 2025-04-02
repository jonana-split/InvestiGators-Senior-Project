using Unity.VisualScripting;
using UnityEngine;

public class scaredenemy : MonoBehaviour
{
    private int hp = 2;
    private BoxCollider2D box;
    private Rigidbody2D rb;
    bool slowed = false;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float shootWhen = 3f;
    float shootCount  = 0.0f;
    float moveCool = 2;
    float moveTime = 0;
    bool freeze = false;
    float freezeCool = .5f;
    Vector2 moveDir = Vector2.one.normalized;
    public GameObject bulletPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveTime = moveCool;
        rb = GetComponent<Rigidbody2D>();
    }
    void randomizeMoveDir()
    {
        moveTime = 0;
        Vector2 pos = transform.position;
        //Debug.Log(pos);
        if (pos.x >= 0 && pos.y >= 0) {
            moveDir = new Vector2(Random.Range(-100, 0), Random.Range(-100, 0)).normalized;
        } 
        else if (pos.x <= 0 && pos.y >= 0)
        {
            moveDir = new Vector2(Random.Range(0, 100), Random.Range(-100, 0)).normalized;
        }
        else if (pos.x >= 0 && pos.y <= 0)
        {
            moveDir = new Vector2(Random.Range(-100, 0), Random.Range(0, 100)).normalized;
        }else
        {
            moveDir = new Vector2(Random.Range(0,100), Random.Range(0,100)).normalized;
        }
        //Debug.Log(moveDir);
        rb.linearVelocity = moveDir * speed;
    }
    // Update is called once per frame
    void Update()
    {

        //Debug.Log(moveDir + " " + rb.linearVelocity);
        moveTime += Time.deltaTime;
        if(freeze)
        {
            if(moveTime >= freezeCool)
            {
                freeze = false;
                randomizeMoveDir();
       
            }
        }else if(moveTime>=moveCool)
        {
            moveTime = 0;
            shoot();
        }

    }
    void shoot()
    {
        for (int i = 0; i < 4; i++)
        {
            var tmpBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            tmpBullet.GetComponent<bullet>().dir = Quaternion.Euler(0, 0, 360 * i / 4.0f+45) * Vector2.up ;
        }
        freeze = true;
        rb.linearVelocity = Vector2.zero;

    }
    public void hurtboxHit(Collider2D collider)
    {
        //Debug.Log("test");
        if (collider.gameObject.tag == "playerBullet")
        {
            Destroy(collider.gameObject);
            hp--;
            if (hp <= 0)
            {
                Destroy(gameObject);
                combatmanager.enemyCount--;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        moveDir = Vector2.Reflect(moveDir, collision.GetContact(0).normal);
        rb.linearVelocity = moveDir * speed;

    }

}
