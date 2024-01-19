using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidRainBehaviour : WeaponController
{
    protected override void Attack()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        Vector3 playerPosition = player.transform.position;
        float randomAngle = Random.Range(0f, 360f);
        Vector3 randomDirection = new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle), 0f);
        float distance = Random.Range(4, 6);
        Vector3 randomPosition = playerPosition + randomDirection * distance;

        GameObject acidOrb = Instantiate(weaponData.Sprite, transform.position, Quaternion.identity);

        acidOrb.GetComponent<AcidOrb>().targetPosition = randomPosition;
        acidOrb.GetComponent<AcidOrb>().startPosition = transform.position;

        acidOrb.GetComponent<AcidOrb>().damage = weaponData.Damage;
        acidOrb.GetComponent<AcidOrb>().speed = weaponData.Speed;


        timer = 0f;
    }
}
