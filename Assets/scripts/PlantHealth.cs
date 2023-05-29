using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float damagePerSecond = 10f;
    float radius = 0.75f;
    private float currentHealth;
    public GameObject gridData;
    int zombieCount = 0;
    plantsCount pc;
    void Start()
    {
        currentHealth = maxHealth;
        pc = FindAnyObjectByType<plantsCount>();
    }
    void Update()
    {
        if (IsBeingAttacked())
        {
            currentHealth -= damagePerSecond * Time.deltaTime;
            if (currentHealth <= 0)
            {
                pc.decPlant();
                Die();
            }
        }
    }
    void Die()
    {
        Vector2 gridPosition= transform.position;
        Vector2Int gridPos = new Vector2Int(Mathf.RoundToInt(gridPosition.x), Mathf.RoundToInt(gridPosition.y));
        print("plant pos in plant health: " + gridPos);
        if (gridData.GetComponent<gridListData>().occupiedPositions.Contains(gridPos))
        {
            print("grid contains");
            gridData.GetComponent<gridListData>().occupiedPositions.Remove(gridPos);
        }
        else
        {
            print("not found");
        }
        Destroy(gameObject);
    }

    bool IsBeingAttacked()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Zombie"))
            {
                return true;
            }
        }
        return false;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
        {
            zombieCount++;
            if(zombieCount==1)
            InvokeRepeating("setAlphaa", 0.1f, 0.3f);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
        {
            if (zombieCount < 2)
            {
                CancelInvoke("setAlphaa");
                CancelInvoke("setAlpha");
                Color color = gameObject.GetComponent<SpriteRenderer>().color;
                color.a = 1f;
                gameObject.GetComponent<SpriteRenderer>().color = color;
            }
            zombieCount--;
        }
    }
    void setAlphaa()
    {
        Color color = gameObject.GetComponent<SpriteRenderer>().color;
        color.a = 0.5f;
        gameObject.GetComponent<SpriteRenderer>().color = color;
        InvokeRepeating("setAlpha", 0.1f, 0.3f);
    }
    void setAlpha()
    {
        Color color = gameObject.GetComponent<SpriteRenderer>().color;
        color.a = 1f;
        gameObject.GetComponent<SpriteRenderer>().color = color;
    }
}
