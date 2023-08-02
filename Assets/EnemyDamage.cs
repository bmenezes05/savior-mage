using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damageAmount = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {                
        if (other.tag == "PlayerHitbox")
        {
            PlayerHealth playerHealth = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerHealth>();            
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
        }
    }
}