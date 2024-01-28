using UnityEngine;

public class WiggleAndRotate : MonoBehaviour
{
    public float wiggleAmount = 0.5f; // Amount of wiggle
    public float wiggleSpeed = 2.0f; // Speed of wiggle
    public float rotationAmount = 5.0f; // Amount of rotation
    public float rotationSpeed = 1.0f; // Speed of rotation

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Start()
    {
        // Store the initial position and rotation
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    void Update()
    {
        Wiggle();
        Rotate();
    }

    void Wiggle()
    {
        // Calculate wiggle offset
        float wiggleOffset = Mathf.Sin(Time.time * wiggleSpeed) * wiggleAmount;
        transform.position = initialPosition + transform.right * wiggleOffset;
    }

    void Rotate()
    {
        // Calculate rotation offset
        float rotationOffset = Mathf.Sin(Time.time * rotationSpeed) * rotationAmount;
        transform.rotation = initialRotation * Quaternion.Euler(0, 0, rotationOffset);
    }
}