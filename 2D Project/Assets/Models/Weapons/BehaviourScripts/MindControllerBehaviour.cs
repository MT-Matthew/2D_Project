using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MindControllerBehaviour : WeaponController
{
    protected override void Attack()
    {
        Vector3 centerPosition = transform.position;
        Vector3 rightPosition = centerPosition + Vector3.right * 10;
        Vector3 leftPosition = centerPosition + Vector3.left * 10;

        GameObject leftLaser = Instantiate(weaponData.Sprite, leftPosition, Quaternion.identity);
        GameObject rightLaser = Instantiate(weaponData.Sprite, rightPosition, Quaternion.identity);

        leftLaser.GetComponent<Laser>().isLeft = true;
        rightLaser.GetComponent<Laser>().isRight = true;

        // leftLaser.transform.parent = transform;
        // rightLaser.transform.parent = transform;

        Destroy(leftLaser, 1);
        Destroy(rightLaser, 1);


        timer = 0f;
    }
}
