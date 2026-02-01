using UnityEngine;

public class PlayerMoveTest : MonoBehaviour
{
    private Rigidbody rb;
    private SpriteRenderer sprite;
    [SerializeField] private float moveSpeed = 5f;
    private Vector3 moveDirection;
    private float horizontalInput;
    private float verticalInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sprite = GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        //GERAK
        moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        if (horizontalInput > 0) sprite.flipX = false;
        else if (verticalInput < 0) sprite.flipX = true;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * moveSpeed;
    }
}