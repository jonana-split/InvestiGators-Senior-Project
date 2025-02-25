using System;
using Unity.VisualScripting;
using UnityEngine;
public class enemyHurtbox : MonoBehaviour
{
    [SerializeField] string parentScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        transform.parent.GetComponent<scaredenemy>().hurtboxHit(collision.collider);
    }
}
