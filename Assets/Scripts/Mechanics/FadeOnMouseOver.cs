using UnityEngine;

public class FadeOnMouseMove2D : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public float fadeAmount = 0.005f; // Adjust this value for faster or slower fading
    private bool isMouseOver = false;
    private Vector3 lastMousePosition;
    public bool isFullyTransparent = false; // This variable becomes true when the object is fully transparent

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isMouseOver && !isFullyTransparent)
        {
            Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float distanceMoved = Vector3.Distance(lastMousePosition, currentMousePosition);

            if (distanceMoved > 0)
            {
                Color color = spriteRenderer.color;
                color.a = Mathf.Max(color.a - fadeAmount * distanceMoved, 0); // Decrease alpha based on movement
                spriteRenderer.color = color;

                if (color.a == 0)
                {
                    isFullyTransparent = true; // Set to true when sprite is fully transparent
                }
            }

            lastMousePosition = currentMousePosition;
        }
    }

    void OnMouseEnter()
    {
        isMouseOver = true;
        lastMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnMouseExit()
    {
        isMouseOver = false;
    }
}