using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [Header("Patrol")]
    public Transform pointA;
    public Transform pointB;
    public float waitTime = 1f;

    [Header("Chase")]
    public Transform player;
    public float chaseDistance = 5f;
    public float stopChaseDistance = 7f;
<<<<<<< Updated upstream
    public float stopDistance = 1.2f;
=======
    public float stopDistance = 1.2f; 
>>>>>>> Stashed changes

    private NavMeshAgent agent;
    private SpriteRenderer sprite;

    private Transform targetPoint;
    private bool isWaiting;
    private float waitCounter;
    private bool isChasing;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        sprite = GetComponentInChildren<SpriteRenderer>();

        // 🔧 FIX ANIMATOR NULL
        if (animator == null)
            animator = GetComponentInChildren<Animator>();

        targetPoint = pointB;

        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // ===== MASUK MODE CHASE =====
        if (distanceToPlayer <= chaseDistance)
            isChasing = true;

        // ===== KELUAR MODE CHASE =====
        if (distanceToPlayer >= stopChaseDistance)
        {
            if (isChasing)
            {
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

        // ===== ANIMASI (AMAN) =====
        if (animator != null)
            animator.SetBool("IsRun", agent.velocity.magnitude > 0.1f);

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
            agent.ResetPath();
        }
    }

    void ChasePlayer(float distance)
    {
<<<<<<< Updated upstream
=======
        
>>>>>>> Stashed changes
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
        if (agent.velocity.x < 0.1f)
            sprite.flipX = false;
        else if (agent.velocity.x > -0.1f)
            sprite.flipX = true;
    }
}
