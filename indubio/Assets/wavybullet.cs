using UnityEngine;

public class wavybullet : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private int speed = 2;
    public Vector2 dir = new Vector2(0, 1);
    private Vector2 dir2 = new Vector2(0, 1);
    int maxAngle = 90;
    int startAngle = -90;
    float time = 0; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dir = dir.normalized;

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        
        dir2 = Quaternion.Euler(0, 0, Mathf.Lerp(startAngle, maxAngle, time*10)) * dir;
        if(time*10 >= 1)
        {
            maxAngle *= -1;
            startAngle *= -1;
            time = 0;
        }
        rb.linearVelocity = speed * (1+time * 10)* dir2;

    }
    private void OnBecameInvisible()
    {
        //Debug.Log("Bye");
        Destroy(gameObject);

    }
}
