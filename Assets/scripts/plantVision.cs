using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plantVision : MonoBehaviour
{
    
    public GameObject ballPrefab;
    public float ballSpeed = 10f;
    public float fireRate = 1f;
    public float damage = 10f;
    public string zombieTag = "Zombie";
    public GameObject visionObject;
    public LayerMask zombieLayerMask;
    public int ballsPerShot = 1;
    float nextFireTime = 0f;
    float visionRange;

    void Update()
    {
        visionRange = Vector2.Distance(transform.position, visionObject.transform.position);
        if (Time.time >= nextFireTime)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, visionRange, zombieLayerMask);
            if (hit.collider != null && hit.collider.CompareTag(zombieTag))
            {
                print("hit by zombie");
                nextFireTime = Time.time + 1f / fireRate;
                FireBall();                
            }
        }
    }
    private void FireBall()
    {
        for (int i = 0; i < ballsPerShot; i++)
        {
            GameObject ball = Instantiate(ballPrefab, transform.position + new Vector3(i * 0.7f, 0f, 0f), Quaternion.identity);
            Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
            ballRb.velocity = transform.right * ballSpeed;
        }
    }
}

