using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyBall : MonoBehaviour
{
    public float damage = 10f;
    public string zombieTag = "Zombie";
    public float maxDistance = 10f;
    private float distanceTraveled = 0f;

    void Update()
    {
        distanceTraveled += Time.deltaTime * GetComponent<Rigidbody2D>().velocity.magnitude;
        if (distanceTraveled >= maxDistance)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(zombieTag))
        {
            print("ball collides with zombie");
            ZombieMovement zm= collision.gameObject.GetComponent<ZombieMovement>();
            zm.TakeDamage(damage);
            
            Destroy(gameObject);
        }
    }
    
}


