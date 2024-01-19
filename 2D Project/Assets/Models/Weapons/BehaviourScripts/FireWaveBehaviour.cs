using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWaveBehaviour : WeaponController
{
    protected override void Attack()
    {
        GameObject waveObject = Instantiate(weaponData.Sprite, transform.position, Quaternion.identity);
        // waveObject.transform.parent = transform;

        Wave waveComponent = waveObject.GetComponent<Wave>();
        waveComponent.maxRadius = weaponData.AttackRadius;
        waveComponent.speed = weaponData.Speed;


        timer = 0f;
    }
}
