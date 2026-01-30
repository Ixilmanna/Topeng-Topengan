using UnityEngine;

public class EnemyPatrolChase : MonoBehaviour
{
    [Header("Patrol")]
    public Transform pointA;
    public Transform pointB;
    public float patrolSpeed = 2f;

    [Header("Chase")]
    public Transform player;
    public float chaseSpeed = 4f;
    public float chaseDistance = 5f;
    public float stopChaseDistance = 7f;
    public float stopDistance = 1.2f; // 🔥 Jarak berhenti dari player

    [Header("Wait")]
    public float waitTime = 1f;

    private Rigidbody rb;
    private Transform targetPoint;
    private bool isWaiting;
    private float waitCounter;

    private float originalScaleX;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetPoint = pointB;
        originalScaleX = transform.localScale.x;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // ===== CHASE PLAYER =====
        if (distanceToPlayer <= chaseDistance)
        {
            ChasePlayer(distanceToPlayer);
            return;
        }

        // ===== BALIK PATROL =====
        if (distanceToPlayer >= stopChaseDistance)
        {
            Patrol();
        }
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

        Move(targetPoint.position, patrolSpeed);

        float distance = Vector3.Distance(transform.position, targetPoint.position);

        if (distance < 0.2f)
        {
            rb.linearVelocity = Vector3.zero;
            isWaiting = true;
            waitCounter = waitTime;
        }
    }

    void ChasePlayer(float distance)
    {
        // 🔥 STOP kalau terlalu dekat
        if (distance <= stopDistance)
        {
            rb.linearVelocity = Vector3.zero;
            return;
        }

        Move(player.position, chaseSpeed);
    }

    void Move(Vector3 target, float speed)
    {
        Vector3 direction = (target - transform.position).normalized;

        rb.linearVelocity = new Vector3(direction.x * speed, 0f, direction.z * speed);

        Flip(direction.x);
    }

    void SwitchTarget()
    {
        targetPoint = targetPoint == pointA ? pointB : pointA;
    }

    void Flip(float dirX)
    {
        if (dirX > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(originalScaleX), transform.localScale.y, transform.localScale.z);
        }
        else if (dirX < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(originalScaleX), transform.localScale.y, transform.localScale.z);
        }
    }
}
