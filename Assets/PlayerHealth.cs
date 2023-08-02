using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public GameObject floatingDamagePrefab; // Refer�ncia para o objeto de dano flutuante

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            ShowFloatingDamage(damageAmount);
        }
    }

    private void Die()
    {
        // Aqui voc� pode colocar as a��es que ser�o realizadas quando o jogador morrer.
        // Por exemplo, carregar uma tela de game over, reiniciar o jogo, etc.
        //Debug.Log("O jogador morreu!");
        // Coloque aqui o c�digo para lidar com a morte do jogador.
    }

    private void ShowFloatingDamage(int damageAmount)
    {
        GameObject floatingDamage = Instantiate(floatingDamagePrefab, transform.position, Quaternion.identity);
        FloatingDamageText damageText = floatingDamage.GetComponent<FloatingDamageText>();
        if (damageText != null)
        {
            damageText.ShowDamage(damageAmount);
        }
    }
}
