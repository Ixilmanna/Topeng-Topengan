using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody rb;
    private SpriteRenderer sprite;
    private Vector3 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sprite = GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // GERAK 8 ARAH
        moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        // FLIP DARI SCALE SAJA
        if (horizontal > 0) sprite.flipX = false;
        else if (horizontal < 0) sprite.flipX = true;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * moveSpeed;
    }
}