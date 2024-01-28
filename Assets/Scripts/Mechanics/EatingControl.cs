using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Events;
using MechanicEvents;
using Mechanics;
using Roro.Scripts.GameManagement;

public class EatingControl : MonoBehaviour
{
    public float moveSpeed;
    public float glitchSpeed;
    public float glitchChance;
    private Vector3 mousePosition;
    private Vector3 moveDirection;

    void Start()
    {
        // Initialize with random values
        SetRandomMovement();
    }

    void Update()
    {
        // Handle input for movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        moveDirection = new Vector3(horizontalInput, verticalInput, 0f).normalized;

        // Move mainly with WASD input
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // Occasionally glitch and move in random directions
        if (Random.value < glitchChance)
        {
            GlitchMovement();
        }

        // Update mouse position
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
    }

    void SetRandomMovement()
    {
        // Set random glitch parameters
        glitchSpeed = Random.Range(5f, 10f);
        glitchChance = Random.Range(0.1f, 0.6f);
    }

    void GlitchMovement()
    {
        // Move in a random direction for a short duration
        Vector3 glitchDirection = Random.insideUnitCircle.normalized;
        transform.Translate(glitchDirection * glitchSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Load Chewing Animation
        // Load Happy Animation
        // Win Condition
        using var evt = MechanicResultEvent.Get(true);
        evt.SendGlobal();
        GetComponent<EatingControl>().enabled = false;
        Debug.Log("Yum!");
    }
}
