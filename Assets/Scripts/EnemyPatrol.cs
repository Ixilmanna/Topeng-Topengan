using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    public enum EnemyState
    {
        Patrol,
        Chase
    }

    [Header("Patrol")]
    public Transform pointA;
    public Transform pointB;
    public float waitTime = 1f;

    [Header("Chase")]
    public Transform player;
    public float chaseDistance = 5f;
    public float stopChaseDistance = 7f;
    public float stopDistance = 1.2f;

    private NavMeshAgent agent;
    private SpriteRenderer sprite;

    private Transform targetPoint;
    private bool isWaiting;
    private float waitCounter;

    private EnemyState currentState;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        sprite = GetComponentInChildren<SpriteRenderer>();

        targetPoint = pointB;
        currentState = EnemyState.Patrol;

        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // ===== STATE SWITCH =====
        if (distanceToPlayer <= chaseDistance)
        {
            currentState = EnemyState.Chase;
            isWaiting = false;
        }
        else if (distanceToPlayer >= stopChaseDistance)
        {
            if (currentState == EnemyState.Chase)
            {
                agent.ResetPath();
            }

            currentState = EnemyState.Patrol;
        }

        // ===== RUN STATE =====
        switch (currentState)
        {
            case EnemyState.Patrol:
                Patrol();
                break;

            case EnemyState.Chase:
                Chase(distanceToPlayer);
                break;
        }

        Flip();
        DebugInfo(distanceToPlayer);
    }

    void Patrol()
    {
        if (isWaiting)
        {
            waitCounter -= Time.deltaTime;

            if (waitCounter <= 0)
            {
                SwitchTarget();
                isWaiting = false;
            }
            return;
        }

        agent.SetDestination(targetPoint.position);

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            isWaiting = true;
            waitCounter = waitTime;
        }
    }

    void Chase(float distance)
    {
        if (distance <= stopDistance)
        {
            agent.ResetPath();
            return;
        }

        agent.SetDestination(player.position);
    }

    void SwitchTarget()
    {
        targetPoint = targetPoint == pointA ? pointB : pointA;
    }

    void Flip()
    {
        if (agent.velocity.x > 0.1f) sprite.flipX = false;
        else if (agent.velocity.x < -0.1f) sprite.flipX = true;
    }

    // ===== DEBUG INFO =====
    void DebugInfo(float distance)
    {
        Debug.Log(
            "STATE: " + currentState +
            " | Waiting: " + isWaiting +
            " | HasPath: " + agent.hasPath +
            " | Velocity: " + agent.velocity.magnitude.ToString("F2") +
            " | DistanceToPlayer: " + distance.ToString("F2")
        );
    }
}
