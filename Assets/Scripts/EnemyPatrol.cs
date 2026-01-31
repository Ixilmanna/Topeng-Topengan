using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
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

    private bool isChasing; // ⭐ STATE

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        sprite = GetComponentInChildren<SpriteRenderer>();

        targetPoint = pointB;

        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // ===== MASUK MODE CHASE =====
        if (distanceToPlayer <= chaseDistance)
        {
            isChasing = true;
        }

        // ===== KELUAR MODE CHASE =====
        if (distanceToPlayer >= stopChaseDistance)
        {
            if (isChasing)
            {
                // Reset patrol state
                isWaiting = false;
                agent.ResetPath();
            }

            isChasing = false;
        }

        // ===== JALANKAN MODE =====
        if (isChasing)
            ChasePlayer(distanceToPlayer);
        else
            Patrol();

        Flip();
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

    void ChasePlayer(float distance)
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
}
