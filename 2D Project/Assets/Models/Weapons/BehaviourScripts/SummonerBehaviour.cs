using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerBehaviour : WeaponController
{
    protected override void Attack()
    {
        GameObject player2 = GameObject.FindGameObjectWithTag("Player");

        Vector3 player = player2.transform.position;
        float randomAngle = Random.Range(0f, 360f);
        Vector3 randomDirection = new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle), 0f);
        float distance = Random.Range(5, 7);
        Vector3 randomPosition = player + randomDirection * distance;

        GameObject blackHole = Instantiate(weaponData.Sprite, randomPosition, Quaternion.identity);
        blackHole.GetComponent<BlackHole>().damage = weaponData.Damage;
        blackHole.GetComponent<BlackHole>().attractionForce = weaponData.KnockBack;
        blackHole.GetComponent<BlackHole>().radius = weaponData.AttackRadius;
        Destroy(blackHole, weaponData.DecayTime);

        timer = 0f;
    }
}
