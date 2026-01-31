using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;

    [Header("Dash")]
    public float dashDistance = 5f;
    public float dashDuration = 0.1f;
    public float dashCooldown = 5f;
    private bool isDashing;
    private bool canDash = true;
    private Vector3 dashDir;
    private float dashTimer;
    private float cooldownDashTimer;

    private Rigidbody rb;
    private SpriteRenderer sprite;


    [Header("Collider")]
    private Collider playerCollider;
    private Collider enemyCollider;

    [SerializeField] private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sprite = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<Collider>();
        IgnoreEnemyCollision(true);
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // GERAK 8 ARAH
        moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        animator.SetFloat("Speed", moveDirection.magnitude);

        flipSprite();

        // Dash input
        if (Input.GetKeyDown(KeyCode.Space) && moveDirection != Vector3.zero && canDash)
        {
            StartDash();
        }

        // Cooldown countdown (ALWAYS runs when locked)
        if (!canDash)
        {
            cooldownDashTimer -= Time.deltaTime;
            Debug.Log("Still Cooldown");
            if (cooldownDashTimer <= 0f)
            {
                Debug.Log("Now Dash!!!");
                canDash = true;
            }
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            dashTimer -= Time.fixedDeltaTime;

            Vector3 dashStep = dashDir * (dashDistance / dashDuration) * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + dashStep);

            if (dashTimer <= 0f)
            {
                isDashing = false;
                IgnoreEnemyCollision(false);
            }

            return;
        }

        rb.linearVelocity = moveDirection * moveSpeed;
    }

    void flipSprite()
    {
        if (horizontalInput > 0) sprite.flipX = false;
        else if (horizontalInput < 0) sprite.flipX = true;
    }

    void StartDash()
    {
        isDashing = true;
        canDash = false;

        dashDir = moveDirection;
        dashTimer = dashDuration;
        cooldownDashTimer = dashCooldown;

        IgnoreEnemyCollision(true);
    }

    void IgnoreEnemyCollision(bool ignore)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            enemyCollider = enemy.GetComponent<Collider>();
            if (enemyCollider != null && playerCollider != null)
            {
                Physics.IgnoreCollision(playerCollider, enemyCollider, ignore);
            }
        }
    }
}

