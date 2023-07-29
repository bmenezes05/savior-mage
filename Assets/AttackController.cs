using UnityEngine;

public class AttackController : MonoBehaviour
{
    Vector2 rightAttackOffset;
    public float damage = 3;
    public Collider2D attackCollider;

    private void Start()
    {        
        rightAttackOffset = transform.position;
    }

    public void AttackRight()
    {        
        attackCollider.enabled = true;
        transform.localPosition = rightAttackOffset;
    }

    public void AttackLeft()
    {
        attackCollider.enabled = true;
        transform.localPosition = new Vector3(rightAttackOffset.x * -1, rightAttackOffset.y);
    }

    public void StopAttack()
    {
        attackCollider.enabled = false;
    }

    private void OnTriggerEnter2D (Collider2D other)
    {        
        if (other.tag == "Enemy")
        {
            EnemyController enemyController = other.GetComponent<EnemyController>();
            
            if (enemyController != null)
            {
                enemyController.Health -= damage;                
            }
        }
    }
}
