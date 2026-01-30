using UnityEngine;

public class SemiTopDownCamera : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Camera Settings")]
    public float height = 10f;        // Tinggi kamera
    public float distance = 8f;       // Jarak ke belakang player
    public float followSpeed = 6f;

    [Header("Camera Angle")]
    [Range(20f, 60f)]
    public float angle = 35f;         // Sudut kemiringan kamera

    void LateUpdate()
    {
        if (target == null) return;

        // Posisi kamera (belakang + atas player)
        Vector3 offset = new Vector3(
            0f,
            height,
            -distance
        );

        Vector3 desiredPosition = target.position + offset;

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            followSpeed * Time.deltaTime
        );

        // Kamera menghadap ke player dengan sudut miring
        transform.rotation = Quaternion.Euler(angle, 0f, 0f);
    }
}
