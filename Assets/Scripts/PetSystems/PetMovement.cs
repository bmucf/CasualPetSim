using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class PetMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 1.5f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private Vector2 movementAreaSize = new Vector2(5f, 5f); // Width x Depth
    [SerializeField] private Vector3 movementAreaCenter = Vector3.zero;

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

        currentTarget = GetRandomPointInArea();
        agent.SetDestination(currentTarget);

        float walkDuration = Random.Range(minWalkTime, maxWalkTime);
        float elapsedTime = 0f;

        while (elapsedTime < walkDuration)
        {
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                currentTarget = GetRandomPointInArea();
                agent.SetDestination(currentTarget);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private Vector3 GetRandomPointInArea()
    {
        Vector3 randomPoint = movementAreaCenter +
            new Vector3(Random.Range(-movementAreaSize.x / 2f, movementAreaSize.x / 2f),
                        0f,
                        Random.Range(-movementAreaSize.y / 2f, movementAreaSize.y / 2f));

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            return hit.position;
        }

        return transform.position; // fallback
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(movementAreaCenter, new Vector3(movementAreaSize.x, 0.1f, movementAreaSize.y));
    }
}