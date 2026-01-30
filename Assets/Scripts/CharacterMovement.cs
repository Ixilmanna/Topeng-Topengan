using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody rb;
    private Vector3 moveDirection;
    private float originalScaleX;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Simpan scale awal
        originalScaleX = transform.localScale.x;
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // GERAK 8 ARAH
        moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        // FLIP DARI SCALE SAJA
        if (horizontal > 0)
        {
            transform.localScale = new Vector3(
                Mathf.Abs(originalScaleX),
                transform.localScale.y,
                transform.localScale.z
            );
        }
        else if (horizontal < 0)
        {
            transform.localScale = new Vector3(
                -Mathf.Abs(originalScaleX),
                transform.localScale.y,
                transform.localScale.z
            );
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * moveSpeed;
    }
}
