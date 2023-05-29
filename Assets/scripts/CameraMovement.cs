using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CameraMovement : MonoBehaviour
{
    public float speed = 1.0f;
    public float distance = 5.0f;

    private Vector3 startPosition;
    private bool movingRight = true;

    void Start()
    {
        // Get the starting position of the camera
        startPosition = transform.position;
    }

    void Update()
    {
        // Move the camera left to right or right to left
        if (movingRight)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        // Check if the camera has moved the desired distance
        if (Vector3.Distance(startPosition, transform.position) >= distance)
        {
            // Reverse the direction of movement
            movingRight = !movingRight;
        }

        // If the camera has reached its starting position, stop moving
        if (!movingRight && Vector3.Distance(startPosition, transform.position) < 0.1f)
        {
            speed = 0f;
            SceneManager.LoadScene(2);
        }
    }
}


