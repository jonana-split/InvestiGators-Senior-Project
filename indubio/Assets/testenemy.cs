using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class testenemy : MonoBehaviour
{
    private int hp = 2;
    private BoxCollider2D box;
    private Vector2 moveDelta;
    private InputAction move;
    private Rigidbody2D rb;
    bool slowed = false;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float speed2 = 2.5f;
    [SerializeField] private float shootWhen = 3f;
    float shootCount  = 0.0f;

    public GameObject bulletPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        shootCount += Time.deltaTime;
        if(shootCount>shootWhen)
        {
            shootCount = 0;
            shoot();

        }
    }
    void shoot()
    {

        Instantiate(bulletPrefab, transform.position, Quaternion.identity);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("test");
        Debug.Log(collision.gameObject.name);
        hp--;
        if(hp <= 0) {
            Destroy(gameObject);
        }
    }

}
