using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private LifeSystem playerHealth;
    [SerializeField] private Image totalhealthBar;
    [SerializeField] private Image currenthealthBar;

    private void Start()
    {
        totalhealthBar.fillAmount = 1f;
    }

    private void Update()
    {
        float healthValue = (float)playerHealth.currentLife / playerHealth.maxLife;
        currenthealthBar.fillAmount = healthValue;
    }
}
