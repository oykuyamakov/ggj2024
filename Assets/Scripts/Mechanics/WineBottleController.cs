using UnityEngine;

public class WineBottleController : MonoBehaviour
{
    public GameObject dropPrefab; // Assign the drop prefab in the inspector
    public GameObject objectToInstantiate; // Publicly assignable object to be instantiated
    public float spawnInterval = 1f; // Time between drop spawns
    public float initialMoveSpeed = 5f; // Initial speed of bottle movement
    public float minX, maxX; // Min and max X positions for bottle movement
    public float minChangeTime = 2f; // Minimum time before changing direction/speed
    public float maxChangeTime = 5f; // Maximum time before changing direction/speed
    public int maxDrops = 10; // Maximum number of drops to spawn
    public float moveOutOfFrameSpeed = 3f; // Speed at which the bottle moves out of frame

    private float spawnTimer;
    private float changeDirectionTimer;
    private float currentSpeed;
    private bool movingRight;
    private int dropsSpawned = 0; // Counter for the number of drops spawned
    private bool stopSpawning = false; // Flag to stop spawning

    void Start()
    {
        spawnTimer = spawnInterval;
        SetNewDirectionAndSpeed();
    }

    void Update()
    {
        if (!stopSpawning)
        {
            MoveBottle();
            HandleDropSpawn();
            HandleRandomMovement();
        }
        else
        {
            MoveOutOfFrame();
        }
    }

    void MoveBottle()
    {
        float movement = currentSpeed * Time.deltaTime * (movingRight ? 1 : -1);
        float newXPosition = Mathf.Clamp(transform.position.x + movement, minX, maxX);
        transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);

        if (newXPosition <= minX || newXPosition >= maxX)
        {
            movingRight = !movingRight; // Reverse direction at edges
            Debug.Log("Bottle hit edge and changed direction");
        }
    }

    void HandleDropSpawn()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0f && dropsSpawned < maxDrops)
        {
            Instantiate(dropPrefab, transform.position, Quaternion.identity);
            spawnTimer = spawnInterval;
            dropsSpawned++;

            if (dropsSpawned >= maxDrops)
            {
                stopSpawning = true;
            }
        }
    }

    void HandleRandomMovement()
    {
        if (!stopSpawning)
        {
            changeDirectionTimer -= Time.deltaTime;

            if (changeDirectionTimer <= 0f)
            {
                SetNewDirectionAndSpeed();
            }
        }
    }

    void SetNewDirectionAndSpeed()
    {
        movingRight = Random.value > 0.5f;
        currentSpeed = Random.Range(0.5f * initialMoveSpeed, 1.5f * initialMoveSpeed);
        changeDirectionTimer = Random.Range(minChangeTime, maxChangeTime);

        Debug.Log($"Direction change: {(movingRight ? "Right" : "Left")}, Speed: {currentSpeed}, Next change in: {changeDirectionTimer} seconds");
    }

    void MoveOutOfFrame()
    {
        // Move the bottle up at a constant speed
        transform.position += Vector3.up * moveOutOfFrameSpeed * Time.deltaTime;

        // Check if the bottle is out of the camera view and instantiate the object
        if (!IsObjectInView())
        {
            InstantiateObjectAtScreenCenter();
            Destroy(gameObject); // Destroy the bottle
        }
    }

    bool IsObjectInView()
    {
        var screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        return screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
    }

    void InstantiateObjectAtScreenCenter()
    {
        // Instantiate the object at the center of the screen
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane);
        Vector3 worldCenter = Camera.main.ScreenToWorldPoint(screenCenter);
        Instantiate(objectToInstantiate, worldCenter, Quaternion.identity);
    }
}
