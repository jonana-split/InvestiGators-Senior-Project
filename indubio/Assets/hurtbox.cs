using UnityEngine;

public class hurtbox : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.parent.gameObject.SendMessage("hurtboxHit", collision);
    }
    public void hitPlayer()
    {
        transform.parent.gameObject.SendMessage("hitPlayer", SendMessageOptions.DontRequireReceiver);
    }
}
