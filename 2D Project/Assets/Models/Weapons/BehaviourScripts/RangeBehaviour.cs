// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class RangeBehaviour : WeaponController
// {
//     protected override void Attack()
//     {
//         GameObject player = GameObject.FindGameObjectWithTag("Player");
//         PlayerController playerController = player.GetComponent<PlayerController>();

//         Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//         Vector2 attackDirection = (mousePosition - transform.position).normalized;

//         GameObject bullet = Instantiate(weaponData.Sprite, transform.position, Quaternion.identity);
//         bullet.GetComponent<Stat>().damage = weaponData.Damage * playerController.currentDamage;
//         bullet.GetComponent<Stat>().knockBack = weaponData.KnockBack;
//         bullet.GetComponent<Rigidbody2D>().velocity = (attackDirection * weaponData.Speed).normalized * weaponData.Speed;

//         // Destroy(bullet, weaponData.DecayTime);

//         timer = 0f;
//     }
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeBehaviour : WeaponController
{
    protected override void Attack()
    {

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 attackDirection = (mousePosition - transform.position).normalized;
        attackDirection.z = 0;

        GameObject bullet = Instantiate(weaponData.Sprite, transform.position, Quaternion.identity);
        bullet.GetComponent<Stat>().damage = weaponData.Damage;
        bullet.GetComponent<Stat>().speed = weaponData.Speed;
        bullet.GetComponent<Stat>().knockBack = weaponData.KnockBack;

        bullet.GetComponent<Bullet>().direction = attackDirection;

        bullet.GetComponent<Rigidbody2D>().velocity = (attackDirection * weaponData.Speed).normalized * weaponData.Speed;

        // Destroy(bullet, weaponData.DecayTime);

        timer = 0f;
    }
}
