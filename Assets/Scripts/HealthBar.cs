using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private LifeSystem playerLife;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;

    void Start()
    {
        if (playerLife == null)
        {
            Debug.LogError("Player LifeSystem belum di assign!");
            return;
        }

        // Set total bar (biasanya full)
        totalHealthBar.fillAmount = 1f;

        Debug.Log("Max Life: " + playerLife.maxLife);
    }

    void Update()
    {
        if (playerLife == null) return;

        float healthValue = (float)playerLife.currentLife / playerLife.maxLife;

        currentHealthBar.fillAmount = healthValue;

        Debug.Log("Current Life: " + playerLife.currentLife);
    }
}
