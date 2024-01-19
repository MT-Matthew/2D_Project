using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event1 : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int numberOfEnemy = 10;
    public float radius = 5f;
    public float duration;

    public float moveSpeed;
    public bool isAlert = false;

    void Start()
    {
        isAlert = GetComponent<EventStats>().isAlert;
        enemyPrefab = GetComponent<EventStats>().enemyPrefab;
        duration = GetComponent<EventStats>().duration;
        SpawnInCircle();
    }

    void SpawnInCircle()
    {
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        for (int i = 0; i < numberOfEnemy; i++)
        {
            float angle = i * Mathf.PI * 2f / numberOfEnemy;
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;

            Vector3 spawnPosition = new Vector3(x, y, 0f) + playerPosition;

            GameObject spawned = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            if (!isAlert)
            {
                spawned.AddComponent<EnemyController>();
                spawned.GetComponent<EnemyController>().eventEnemy = true;

                spawned.GetComponent<EnemyStat>().targetPosition = playerPosition;
                spawned.GetComponent<EnemyStat>().eventMoveSpeed = moveSpeed;
            }

            Destroy(spawned, duration);
        }
    }
}
