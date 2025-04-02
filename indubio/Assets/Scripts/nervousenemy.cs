using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class nervousenemy : MonoBehaviour
{
    private int hp = 2;
    private BoxCollider2D box;
    private Rigidbody2D rb;
    bool slowed = false;
    GameObject player;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float speed2 = 2.5f;
    [SerializeField] private float shootWhen = .5f;
    float shootCount = 0.0f;

    public GameObject bulletPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("playercombat");
    }

    // Update is called once per frame
    void Update()
    {
        shootCount += Time.deltaTime;
        if (shootCount > shootWhen)
        {
            shootCount = 0;
            shoot();

        }
    }
    void shoot()
    {
        var dir = (player.transform.position - transform.position).normalized;
        var tmpBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        tmpBullet.GetComponent<wavybullet>().dir = dir;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("test");
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "playerBullet")
        {
            Destroy(collision.gameObject);
            hp--;
            if (hp <= 0)
            {
                Destroy(gameObject);
                combatmanager.enemyCount--;
            }
        }
    }


}
