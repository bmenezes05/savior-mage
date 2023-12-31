using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public PlayerHealth playerHealth; // ReferÍncia para o script PlayerHealth do jogador.
    public Slider slider; // ReferÍncia para o componente Slider da barra de vida.

    void Start()
    {
        slider.maxValue = playerHealth.maxHealth;
    }

    void Update()
    {
        slider.value = playerHealth.currentHealth;
    }
}
