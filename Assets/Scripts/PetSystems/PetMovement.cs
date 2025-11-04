using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class PetMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 1.5f;
    [SerializeField] private float rotationSpeed = 10f;
    //[SerializeField] private Vector2 movementAreaSize = new Vector2(5f, 5f); // Width x Depth
    //[SerializeField] private Vector3 movementAreaCenter = Vector3.zero;

    [Header("Behavior Settings")]
    [SerializeField] private float minIdleTime = 5f;
    [SerializeField] private float maxIdleTime = 10f;
    [SerializeField] private float minWalkTime = 2f;
    [SerializeField] private float maxWalkTime = 15f;
    [SerializeField] private float waypointReachedDistance = 0.3f;

    [Header("Animation (Optional)")]
    [SerializeField] private Animator animator;
    [SerializeField] private string idleAnimParam = "IsIdle";
    [SerializeField] private string walkAnimParam = "IsWalking";

    private NavMeshAgent agent;
    private Vector3 currentTarget;
    private bool isMoving = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        agent.speed = moveSpeed;
        agent.angularSpeed = rotationSpeed * 100f; // angularSpeed is in degrees/sec
        agent.stoppingDistance = waypointReachedDistance;

        // If you want to handle rotation manually, uncomment:
        // agent.updateRotation = false;

        NavMeshHit hit;
        if (!NavMesh.SamplePosition(transform.position, out hit, 1f, NavMesh.AllAreas))
        {
            Debug.LogWarning("PetMovement: Agent is not on a valid NavMesh surface. Disabling script.");
            this.enabled = false;
            return;
        }

    }

    private void OnEnable()
    {
        StartCoroutine(BehaviorLoop());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator BehaviorLoop()
    {
        while (true)
        {
            yield return StartCoroutine(IdleState());
            yield return StartCoroutine(WalkingState());
        }
    }

    private IEnumerator IdleState()
    {
        isMoving = false;
        SetAnimation(true, false);

        float idleDuration = Random.Range(minIdleTime, maxIdleTime);
        yield return new WaitForSeconds(idleDuration);
    }

    private IEnumerator WalkingState()
    {
        isMoving = true;
        SetAnimation(false, true);

        currentTarget = GetRandomPointOnNavMesh();
        agent.SetDestination(currentTarget);

        float walkDuration = Random.Range(minWalkTime, maxWalkTime);
        float elapsedTime = 0f;

        while (elapsedTime < walkDuration)
        {
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                currentTarget = GetRandomPointOnNavMesh();
                agent.SetDestination(currentTarget);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private void Update()
    {
        // Doesnt Work:
        //// Drive animation from actual velocity
        //if (animator != null)
        //{
        //    Debug.Log("Animator callled");
        //    bool walking = agent.velocity.magnitude > 0.1f;
        //    animator.SetBool(idleAnimParam, !walking);
        //    animator.SetBool(walkAnimParam, walking);
        //}
        

        // Optional: manual rotation smoothing if agent.updateRotation = false
        // if (agent.velocity.sqrMagnitude > 0.1f)
        // {
        //     Quaternion targetRotation = Quaternion.LookRotation(agent.velocity.normalized);
        //     transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        // }
    }

    private void SetAnimation(bool idle, bool walking)
    {
        if (animator != null)
        {
            animator.SetBool(idleAnimParam, idle);
            animator.SetBool(walkAnimParam, walking);
        }
    }

    private Vector3 GetRandomPointOnNavMesh(float radius = 50f, int maxAttempts = 30)
    {
        for (int i = 0; i < maxAttempts; i++)
        {
            // Pick a random point in a sphere around the agent
            Vector3 randomDirection = Random.insideUnitSphere * radius;
            randomDirection += transform.position;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, 2f, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }

        // If no point was found after all attempts, throw an error
        // Debug.LogError("PetMovement: Could not find a valid point on the NavMesh!");
        return transform.position; // fallback to current position
    }
}