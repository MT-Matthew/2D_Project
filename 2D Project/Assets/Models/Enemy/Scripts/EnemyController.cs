using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemySpawner enemySpawn;
    private Counting counting;
    private float playerDamage;

    public bool eventEnemy;
    public bool killByPlayer = false;

    void Start()
    {
        enemySpawn = GameObject.FindGameObjectWithTag("Manager").GetComponent<EnemySpawner>();
        counting = GameObject.FindGameObjectWithTag("Manager").GetComponent<Counting>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Weapon"))
        {
            IDamageable damageableObject = gameObject.GetComponent<IDamageable>();

            if (damageableObject != null)
            {
                Stat impact = other.GetComponent<Stat>();
                Vector2 direction = (other.transform.position - transform.position).normalized;

                Vector2 knockback = direction * impact.knockBack;

                playerDamage = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().currentDamage;
                damageableObject.OnHit(impact.damage * playerDamage, knockback);

                Animator animator = other.GetComponent<Animator>();
                if (animator != null)
                {
                    animator.SetBool("hit", true);
                }
            }
        }
    }

    void OnDestroy()
    {
        if (enemySpawn != null && gameObject.CompareTag("Enemy") && !eventEnemy && killByPlayer)
        {
            enemySpawn.currentEnemyCount--;
            counting.eliminatedCount++;
        }
        else if ((killByPlayer && eventEnemy) || (GetComponent<EnemyStat>().isHarmless))
        {
            counting.eliminatedCount++;
        }

        if (gameObject.CompareTag("Boss"))
        {
            counting.Boss++;
        }
        if (gameObject.CompareTag("Mini Boss"))
        {
            counting.miniBoss++;
        }
    }


}