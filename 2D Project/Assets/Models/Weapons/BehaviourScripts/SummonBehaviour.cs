using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonBehaviour : WeaponController
{
    protected override void Attack()
    {
        GameObject player2 = GameObject.FindGameObjectWithTag("Player");

        Vector3 player = player2.transform.position;
        float randomAngle = Random.Range(0f, 360f);
        Vector3 randomDirection = new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle), 0f);
        float distance = Random.Range(2, 9);
        Vector3 randomPosition = player + randomDirection * distance;

        Vector3 targetPosition = new Vector3(randomPosition.x, randomPosition.y, 0f);
        Vector3 startPosition = new Vector3(randomPosition.x, randomPosition.y + 10, 0f);

        // float camHeight = Camera.main.orthographicSize;
        // float camWidth = camHeight * Camera.main.aspect;
        // Vector3 cameraPosition = Camera.main.transform.position;

        // float spawnX = Random.Range(cameraPosition.x - camWidth, cameraPosition.x + camWidth);
        // float spawnY = Random.Range(cameraPosition.y - camHeight, cameraPosition.y + camHeight);

        // Vector3 targetPosition = new Vector3(spawnX, spawnY, 0f);
        // Vector3 startPosition = new Vector3(spawnX, spawnY + 10, 0f);

        GameObject newObject = Instantiate(weaponData.Sprite, startPosition, Quaternion.identity);
        newObject.GetComponent<Ice>().targetPosition = targetPosition;
        newObject.GetComponent<Ice>().speed = weaponData.Speed;
        newObject.GetComponent<Ice>().damage = weaponData.Damage;
        newObject.GetComponent<Ice>().decayTime = weaponData.DecayTime;

        timer = 0f;
    }
}
