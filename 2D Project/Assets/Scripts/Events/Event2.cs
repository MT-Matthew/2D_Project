using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event2 : MonoBehaviour
{
    public GameObject enemyPrefab;

    public float duration;

    public float spacing = 1f;

    Vector3 playerPosition;

    public int objectsPerHorizontal = 10;
    public int objectsPerVertical = 10;

    public float horizontalSpacing = 5f;
    public float verticalSpacing = 5f;

    public float moveSpeed;
    public bool isAlert = false;

    void Start()
    {
        isAlert = GetComponent<EventStats>().isAlert;
        enemyPrefab = GetComponent<EventStats>().enemyPrefab;
        duration = GetComponent<EventStats>().duration;
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;


        SpawnInRectangle();
    }

    void SpawnInRectangle()
    {
        // Spawn objects above the player
        for (int i = 0; i < objectsPerHorizontal; i++)
        {
            float x = i * spacing - (spacing * (objectsPerHorizontal - 1) / 2);
            float y = horizontalSpacing;
            Vector3 spawnPosition = new Vector3(x, y, 0f) + playerPosition;
            Vector3 targetPosition = new Vector3(spawnPosition.x, spawnPosition.y - 5, 0f);

            GameObject spawned1 = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            if (!isAlert)
            {
                spawned1.AddComponent<EnemyController>();
                spawned1.GetComponent<EnemyController>().eventEnemy = true;

                spawned1.GetComponent<EnemyStat>().targetPosition = targetPosition;
                spawned1.GetComponent<EnemyStat>().eventMoveSpeed = moveSpeed;
            }
            Destroy(spawned1, duration);
        }

        // Spawn objects below the player
        for (int i = 0; i < objectsPerHorizontal; i++)
        {
            float x = i * spacing - (spacing * (objectsPerHorizontal - 1) / 2);
            float y = -horizontalSpacing;
            Vector3 spawnPosition = new Vector3(x, y, 0f) + playerPosition;
            Vector3 targetPosition = new Vector3(spawnPosition.x, spawnPosition.y + 5, 0f);

            GameObject spawned2 = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            if (!isAlert)
            {
                spawned2.AddComponent<EnemyController>();
                spawned2.GetComponent<EnemyController>().eventEnemy = true;

                spawned2.GetComponent<EnemyStat>().targetPosition = targetPosition;
                spawned2.GetComponent<EnemyStat>().eventMoveSpeed = moveSpeed;
            }
            Destroy(spawned2, duration);
        }

        // Spawn objects to the left of the player
        for (int i = 0; i < objectsPerVertical; i++)
        {
            float x = -verticalSpacing;
            float y = i * spacing - (spacing * (objectsPerVertical - 1) / 2);
            Vector3 spawnPosition = new Vector3(x, y, 0f) + playerPosition;
            Vector3 targetPosition = new Vector3(spawnPosition.x + 5f, spawnPosition.y, 0f);

            GameObject spawned3 = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            if (!isAlert)
            {
                spawned3.AddComponent<EnemyController>();
                spawned3.GetComponent<EnemyController>().eventEnemy = true;

                spawned3.GetComponent<EnemyStat>().targetPosition = targetPosition;
                spawned3.GetComponent<EnemyStat>().eventMoveSpeed = moveSpeed;
            }
            Destroy(spawned3, duration);
        }

        // Spawn objects to the right of the player
        for (int i = 0; i < objectsPerVertical; i++)
        {
            float x = verticalSpacing;
            float y = i * spacing - (spacing * (objectsPerVertical - 1) / 2);
            Vector3 spawnPosition = new Vector3(x, y, 0f) + playerPosition;
            Vector3 targetPosition = new Vector3(spawnPosition.x - 5f, spawnPosition.y, 0f);

            GameObject spawned4 = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            if (!isAlert)
            {
                spawned4.AddComponent<EnemyController>();
                spawned4.GetComponent<EnemyController>().eventEnemy = true;

                spawned4.GetComponent<EnemyStat>().targetPosition = targetPosition;
                spawned4.GetComponent<EnemyStat>().eventMoveSpeed = moveSpeed;
            }

            Destroy(spawned4, duration);
        }
    }
}
