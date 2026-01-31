using UnityEngine;
using System.Collections;

public class LifeSystem : MonoBehaviour
{
    public int maxLife = 3;
    public int currentLife;

    public float invincibleTime = 1.5f;
    private bool isInvincible = false;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        currentLife = maxLife;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit: " + collision.gameObject.name
              + " | Tag: " + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Enemy") && !isInvincible)
        {
            Debug.Log("Getting Hit by The Enemy");
            TakeDamage(1);
        }
    }

    void TakeDamage(int damage)
    {
        currentLife -= damage;

        if (currentLife <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(Invincible());
        }
    }

    void Die()
    {
        Debug.Log("Game Over");
        gameObject.SetActive(false);
        // panggil GameOver UI di sini
    }

    IEnumerator Invincible()
    {
        isInvincible = true;

        float blinkDuration = invincibleTime;
        float blinkInterval = 0.1f;

        while (blinkDuration > 0)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            blinkDuration -= blinkInterval;
            yield return new WaitForSeconds(blinkInterval);
        }

        spriteRenderer.enabled = true;
        isInvincible = false;
    }
}
