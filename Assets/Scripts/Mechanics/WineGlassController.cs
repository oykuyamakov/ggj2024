using UnityEngine;

public class WineGlassController : MonoBehaviour
{
    void Update()
    {
        // Convert the mouse position from screen coordinates to world coordinates
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Set the new position of the GameObject
        // Only change the X position, keep the Y and Z position constant
        transform.position = new Vector3(mousePosition.x, transform.position.y, transform.position.z);
    }
}