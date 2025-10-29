using UnityEngine;
using System.Collections;

public class PetMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private Vector2 movementAreaSize = new Vector2(5f, 5f); // Width x Depth
    [SerializeField] private Vector3 movementAreaCenter = Vector3.zero;

    [Header("Behavior Settings")]
    [SerializeField] private float minIdleTime = 1f;
    [SerializeField] private float maxIdleTime = 3f;
    [SerializeField] private float minWalkTime = 2f;
    [SerializeField] private float maxWalkTime = 5f;
    [SerializeField] private float waypointReachedDistance = 0.3f;

    [Header("Obstacle Avoidance")]
    [SerializeField] private bool enableObstacleAvoidance = true;
    [SerializeField] private float detectionDistance = 1f;
    [SerializeField] private float detectionRadius = 0.3f;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private int avoidanceAttempts = 5;

    [Header("Animation (Optional)")]
    [SerializeField] private Animator animator;
    [SerializeField] private string idleAnimParam = "IsIdle";
    [SerializeField] private string walkAnimParam = "IsWalking";

    private Vector3 currentTarget;
    private bool isMoving = false;
    private float stateTimer = 0f;

    void OnEnable()
    {
        // Start with idle state
        StartCoroutine(BehaviorLoop());
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator BehaviorLoop()
    {
        while (true)
        {
            // Idle state
            yield return StartCoroutine(IdleState());

            // Walking state
            yield return StartCoroutine(WalkingState());
        }
    }

    IEnumerator IdleState()
    {
        isMoving = false;
        SetAnimation(true, false);

        float idleDuration = Random.Range(minIdleTime, maxIdleTime);
        yield return new WaitForSeconds(idleDuration);
    }

    IEnumerator WalkingState()
    {
        isMoving = true;
        SetAnimation(false, true);

        // Pick random target within movement area
        currentTarget = GetRandomPointInArea();

        float walkDuration = Random.Range(minWalkTime, maxWalkTime);
        float elapsedTime = 0f;

        while (elapsedTime < walkDuration)
        {
            // Check if reached current target
            float distanceToTarget = Vector3.Distance(transform.position, currentTarget);

            if (distanceToTarget < waypointReachedDistance)
            {
                // Pick new target
                currentTarget = GetRandomPointInArea();
            }

            // Move towards target
            MoveTowardsTarget();

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    void MoveTowardsTarget()
    {
        // Calculate direction to target (only on XZ plane)
        Vector3 direction = currentTarget - transform.position;
        direction.y = 0f;

        if (direction.magnitude > 0.1f)
        {
            // Rotate towards target
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Move forward
            Vector3 newPosition = transform.position + transform.forward * moveSpeed * Time.deltaTime;

            // Keep within bounds
            newPosition = ClampPositionToArea(newPosition);
            transform.position = newPosition;

            newPosition.y = 0.04f; // Keep pet grounded
            transform.position = newPosition;
        }
    }

    Vector3 GetRandomPointInArea()
    {
        float randomX = Random.Range(-movementAreaSize.x / 2f, movementAreaSize.x / 2f);
        float randomZ = Random.Range(-movementAreaSize.y / 2f, movementAreaSize.y / 2f);

        Vector3 randomPoint = movementAreaCenter + new Vector3(randomX, 0f, randomZ);
        randomPoint.y = transform.position.y; // Keep same Y level

        return randomPoint;
    }

    Vector3 ClampPositionToArea(Vector3 position)
    {
        float halfWidth = movementAreaSize.x / 2f;
        float halfDepth = movementAreaSize.y / 2f;

        position.x = Mathf.Clamp(position.x, movementAreaCenter.x - halfWidth, movementAreaCenter.x + halfWidth);
        position.z = Mathf.Clamp(position.z, movementAreaCenter.z - halfDepth, movementAreaCenter.z + halfDepth);

        return position;
    }

    void SetAnimation(bool idle, bool walking)
    {
        if (animator != null)
        {
            animator.SetBool(idleAnimParam, idle);
            animator.SetBool(walkAnimParam, walking);
        }
    }

    // Visualize movement area in editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(movementAreaCenter, new Vector3(movementAreaSize.x, 0.1f, movementAreaSize.y));
    }
}