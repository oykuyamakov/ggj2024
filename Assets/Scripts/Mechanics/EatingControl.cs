using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EatingControl : MonoBehaviour
{
    public float moveSpeed;
    public float moveTime;
    private float timer;
    private Vector3 randomDirection;
    private Vector3 mousePosition;

    void Start()
    {
        // Initialize with random values
        SetRandomMovement();
    }

    void Update()
    {
        // Move in the current direction
        transform.Translate(randomDirection * moveSpeed * Time.deltaTime);

        // Countdown the timer
        timer -= Time.deltaTime;

        // If the timer runs out, set a new random movement
        if (timer <= 0f)
        {
            SetRandomMovement();
        }

        

        // Approach the mouse position
        Vector3 directionToMouse = mousePosition - transform.position;
        transform.Translate(directionToMouse.normalized * moveSpeed * Time.deltaTime);
    }

    void SetRandomMovement()
    {
        // Set random speed, direction, and time
        moveSpeed = Random.Range(0f, 3f);
        float randomAngle = Random.Range(0f, 360f);
        randomDirection = Quaternion.Euler(0f, 0f, randomAngle) * Vector3.up;
        moveTime = Random.Range(0f, 1f);
        timer = moveTime;

        // Update mouse position
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Load Chewing Animation
        Debug.Log("Yum!");
    }
}

