using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinCollect : MonoBehaviour
{
    public int coinValue = 5;
    public float lifetime = 10f;
    private Rigidbody2D rb;
    coinsGenerator c;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        float force = Random.Range(1f, 3f);
        float direction = Random.Range(0f, 1f) > 0.5f ? -1f : 1f;
        rb.AddForce(new Vector2(force * direction, 0f), ForceMode2D.Impulse);
        c = FindAnyObjectByType<coinsGenerator>();
        Destroy(gameObject, lifetime);
    }
    private void OnMouseDown()
    {
        c.increasePoints();
        Destroy(gameObject);
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "coinRemain")
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        }
    }
}
