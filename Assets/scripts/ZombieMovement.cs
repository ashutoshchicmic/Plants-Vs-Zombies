using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ZombieMovement : MonoBehaviour
{
    public float speed = 1f;
    float speedAgain;
    public Vector2 direction = Vector2.left;
    public bool isFrozen=false;
    Rigidbody2D rb2d;
    public float maxHealth = 100f;
    public float freezeTime = 2f;
    private float currentHealth;
    Animator anim;
    public GameObject particlePrefabGreen;

    SpriteRenderer spriteRenderer;
    spawnZomies spZ;
    List<string> tagOfBalls = new List<string> { "GreenBall", "IceBall", "FireBall" };
    bool isDied=false;

    void Start()
    {
        currentHealth = maxHealth;
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spZ = FindAnyObjectByType<spawnZomies>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0f)
        {
            speed = 0;
            anim.SetBool("isDead", true);
            StartCoroutine(Descend(0.9f));
            isDied = true;
            //Invoke("Die", 1f);

        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
    void FixedUpdate()
    {
        if (speed != 0)
        {
            if (spZ.spawnDelay > 1.5f && spZ.spawnDelay <= 2f)
                speed = 1f;
            else if (spZ.spawnDelay > 1f && spZ.spawnDelay <= 1.5f)
                speed = 1.3f;
            else if (spZ.spawnDelay > 0.5f && spZ.spawnDelay <= 1f)
                speed = 1.5f;
            else
                speed = 1.8f;
        }
        print("speed of zombies: " + speed);
        if (!isFrozen)
        rb2d.velocity = direction * speed;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Plant"))
        {
            anim.SetBool("isEating", true);
            print("trigger plant");
            speedAgain = speed;
            speed = 0;
        }
        if (other.CompareTag("IceBall"))
        {
            StartCoroutine(Freeze());
        }

        if (tagOfBalls.Contains(other.gameObject.tag))
        {
            if (speed != 0)
            { 
                GameObject particle = Instantiate(particlePrefabGreen, transform.position, Quaternion.identity);
                Destroy(particle, 0.4f);
            }
            Color color = gameObject.GetComponent<SpriteRenderer>().color;
            color.a = 0.5f;
            gameObject.GetComponent<SpriteRenderer>().color = color;
            Invoke("setAlpha", 0.1f);
        }
    }
    
    IEnumerator Freeze()
    {
       isFrozen = true;
        anim.SetBool("isFrozen", isFrozen);
        rb2d.velocity = Vector2.zero;
        yield return new WaitForSeconds(freezeTime);
        isFrozen = false;
        anim.SetBool("isFrozen", isFrozen);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Plant") && !isDied)
        {
            anim.SetBool("isEating", false);
            print("trigger plant");
            speed = speedAgain;
        }
    }
    void setAlpha()
    {
        Color color = gameObject.GetComponent<SpriteRenderer>().color;
        color.a = 1f;
        gameObject.GetComponent<SpriteRenderer>().color = color;
    }
    IEnumerator Descend(float descendDuration)
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(transform.position.x, transform.position.y - 0.7f, transform.position.z); // change the y-value here to adjust how far down the zombie should descend
        float timeElapsed = 0f;

        while (timeElapsed < descendDuration)
        {
            transform.position = Vector3.Lerp(startPos, endPos, timeElapsed / descendDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject); // destroy the zombie object once the descend is complete
    }

}


