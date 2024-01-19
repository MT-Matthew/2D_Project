using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezerBehaviour : WeaponController
{
    protected override void Attack()
    {
        GameObject player2 = GameObject.FindGameObjectWithTag("Player");

        Vector3 player = player2.transform.position;
        float randomAngle = Random.Range(0f, 360f);
        Vector3 randomDirection = new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle), 0f);
        float distance = Random.Range(4, 7);
        Vector3 randomPosition = player + randomDirection * distance;

        GameObject freezArea = Instantiate(weaponData.Sprite, randomPosition, Quaternion.identity);
        freezArea.GetComponent<FreezeArea>().freezeTime = weaponData.DecayTime;
        freezArea.GetComponent<FreezeArea>().radius = weaponData.AttackRadius;
        Destroy(freezArea, 1);

        timer = 0f;
    }
}
