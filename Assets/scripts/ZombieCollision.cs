using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ZombieCollision : MonoBehaviour
{
    ZombieMovement zm;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("trigger");
        if (collision.gameObject.CompareTag("Plant"))
        {
            print("trigger plant");
            zm.speed = 0;
        }
    }
}


