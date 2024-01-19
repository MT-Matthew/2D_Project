using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraBehaviour : WeaponController
{

    protected override void Attack()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerController playerController = player.GetComponent<PlayerController>();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, weaponData.AttackRadius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                IDamageable damageableObject = collider.GetComponent<IDamageable>();
                if (damageableObject != null)
                {
                    damageableObject.OnHit(weaponData.Damage * playerController.currentDamage);
                }
            }
        }
        timer = 0f;
    }
}
