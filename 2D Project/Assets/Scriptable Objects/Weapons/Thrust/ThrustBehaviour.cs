using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrustBehaviour : WeaponController
{
    protected override void Attack()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");


        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Vị trí chuột trong thế giới 3D
        Vector3 playerPosition = player.transform.position; // Vị trí người chơi
        Vector3 direction = mousePosition - playerPosition; // Hướng từ người chơi tới chuột
        direction.z = 0; // Đảm bảo hướng chỉ trong mặt phẳng 2D
        Vector3 spawnPosition = playerPosition + direction.normalized * 2; // Vị trí để tạo vật thể


        GameObject newObject = Instantiate(weaponData.Sprite, spawnPosition, Quaternion.identity);
        // newObject.transform.parent = transform.parent;
        Destroy(newObject, 0.5f);

        newObject.GetComponent<Thrust>().direction = direction;
        newObject.GetComponent<Stat>().damage = weaponData.Damage;
        newObject.GetComponent<Stat>().knockBack = weaponData.KnockBack;


        timer = 0f;
    }
}
