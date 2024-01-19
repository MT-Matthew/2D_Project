using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreatBehaviour : WeaponController
{
    protected override void Attack()
    {
        GameObject newObject = Instantiate(weaponData.Sprite, transform.position, Quaternion.identity);
        // waveObject.transform.parent = transform;

        Threat threat = newObject.GetComponent<Threat>();
        threat.maxRadius = weaponData.AttackRadius;
        threat.speed = weaponData.Speed;


        timer = 0f;
    }
}
