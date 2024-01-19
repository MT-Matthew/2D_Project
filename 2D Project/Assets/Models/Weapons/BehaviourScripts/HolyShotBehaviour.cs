using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyShotBehaviour : WeaponController
{
    float distance;
    float nearestDistance;
    GameObject nearestEnemy;


    protected override void Attack()
    {
        CheckNearest();

        if (nearestEnemy)
        {
            GameObject holyBullet = Instantiate(weaponData.Sprite, transform.position, Quaternion.identity);
            holyBullet.GetComponent<HolyBullet>().direction = (nearestEnemy.transform.position - transform.position).normalized;
            holyBullet.GetComponent<Stat>().speed = weaponData.Speed;
            holyBullet.GetComponent<Stat>().damage = weaponData.Damage;

        }

        timer = 0f;
    }

    void CheckNearest()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] enemies2 = GameObject.FindGameObjectsWithTag("Mini Boss");

        // Kết hợp danh sách
        List<GameObject> allEnemies = new List<GameObject>();
        allEnemies.AddRange(enemies);
        allEnemies.AddRange(enemies2);

        nearestDistance = 10000;

        foreach (GameObject enemy in allEnemies)
        {
            distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < nearestDistance)
            {
                nearestEnemy = enemy;
                nearestDistance = distance;
            }
        }
    }
}
