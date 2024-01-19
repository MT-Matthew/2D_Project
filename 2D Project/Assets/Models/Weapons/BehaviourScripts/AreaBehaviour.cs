using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaBehaviour : WeaponController
{
    protected override void Attack()
    {
        for (int i = 0; i < weaponData.SummonNum; i++)
        {
            GameObject sawblade = Instantiate(weaponData.Sprite, transform.position, Quaternion.identity);

            sawblade.GetComponent<Stat>().damage = weaponData.Damage;
            sawblade.GetComponent<Stat>().knockBack = weaponData.KnockBack;
            sawblade.GetComponent<Stat>().speed = weaponData.Speed;

            sawblade.GetComponent<Saw>().radius = weaponData.AttackRadius;

            sawblade.GetComponent<Saw>().startAngle = (360f / weaponData.SummonNum) * i;

            Destroy(sawblade, weaponData.DecayTime);
        }


        timer = 0f;
    }

}
