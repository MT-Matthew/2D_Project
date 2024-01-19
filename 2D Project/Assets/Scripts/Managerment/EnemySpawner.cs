using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class EnemyTimeZone
    {
        public GameObject enemyPrefab;
        public int startMinute;
        public int startSecond;
        public int endMinute;
        public int endSecond;
    }

    [System.Serializable]
    public class MiniBossTimeZone
    {
        public GameObject enemyPrefab;
        public int spawnMinute;
        public int spawnSecond;
    }

    public EnemiesListScriptableObject eventsList;
    public GameObject alertPrefab;

    public List<EnemyTimeZone> enemyTimeZone = new List<EnemyTimeZone>();
    public List<MiniBossTimeZone> miniBossTimeZone = new List<MiniBossTimeZone>();

    public Camera mainCamera;
    public int maxEnemies = 10;
    public int currentEnemyCount = 0;
    private int startMaxEnemies;

    private Timer clock;
    private int minuteTimer;
    private int secondTimer;

    void Start()
    {
        clock = GetComponent<Timer>();
        SetSpawnTimer();
        startMaxEnemies = maxEnemies;
    }

    void Update()
    {
        minuteTimer = clock.minuteTimer;
        secondTimer = clock.secondTimer;

        if (currentEnemyCount < maxEnemies)
        {
            int currentTotalMinutes = minuteTimer * 60 + secondTimer;
            List<GameObject> enemyOptions = new List<GameObject>();

            foreach (var enemy in enemyTimeZone)
            {
                int startTotalMinutes = enemy.startMinute * 60 + enemy.startSecond;
                int endTotalMinutes = enemy.endMinute * 60 + enemy.endSecond;
                if (currentTotalMinutes >= startTotalMinutes && currentTotalMinutes <= endTotalMinutes)
                {
                    enemyOptions.Add(enemy.enemyPrefab);
                }
            }

            if (enemyOptions.Count > 0)
            {
                SpawnEnemy(enemyOptions);
            }

        }

        maxEnemies = startMaxEnemies + minuteTimer;
    }

    void SetSpawnTimer()
    {
        foreach (var enemy in miniBossTimeZone)
        {
            int spawnTime = enemy.spawnMinute * 60 + enemy.spawnSecond;
            StartCoroutine(StartSpawnEvent(spawnTime, enemy.enemyPrefab));
        }

        foreach (var ev in eventsList.events)
        {
            int spawnTime2 = ev.spawnMinute * 60 + ev.spawnSecond;

            StartCoroutine(StartAlert(spawnTime2, alertPrefab, ev.eventPrefab));
            StartCoroutine(StartSpawnEvent2(spawnTime2, ev.enemyPrefab, ev.eventPrefab, ev.duration));
        }
    }

    private IEnumerator StartSpawnEvent(int spawnTime, GameObject enemyPrefab)
    {
        yield return new WaitForSeconds(spawnTime);

        Vector3 spawnPosition = GetRandomPosition();
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        // Gắn script EnemyController vào kẻ địch mới tạo để theo dõi việc bị phá hủy
        newEnemy.AddComponent<EnemyController>();
        newEnemy.GetComponent<EnemyController>().eventEnemy = false;
    }

    private IEnumerator StartSpawnEvent2(int spawnTime, GameObject enemyPrefab, GameObject eventPrefab, float duration)
    {
        yield return new WaitForSeconds(spawnTime);

        GameObject newEvent = Instantiate(eventPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        newEvent.transform.parent = gameObject.transform;

        newEvent.GetComponent<EventStats>().enemyPrefab = enemyPrefab;
        newEvent.GetComponent<EventStats>().duration = duration;
        newEvent.GetComponent<EventStats>().isAlert = false;
    }

    private IEnumerator StartAlert(int spawnTime, GameObject alertPrefab, GameObject eventPrefab)
    {
        yield return new WaitForSeconds(spawnTime - 1);

        GameObject.FindGameObjectWithTag("Alert").GetComponent<AudioSource>().Play();

        GameObject newAlert = Instantiate(eventPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        newAlert.transform.parent = gameObject.transform;

        newAlert.GetComponent<EventStats>().enemyPrefab = alertPrefab;
        newAlert.GetComponent<EventStats>().duration = 1f;
        newAlert.GetComponent<EventStats>().isAlert = true;

        Destroy(newAlert, 1.1f);
    }

    void SpawnEnemy(List<GameObject> enemyPrefabs)
    {
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];

        Vector3 spawnPosition = GetRandomPosition();

        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        // Gắn script EnemyController vào kẻ địch mới tạo để theo dõi việc bị phá hủy
        newEnemy.AddComponent<EnemyController>();
        newEnemy.GetComponent<EnemyController>().eventEnemy = false;

        currentEnemyCount++;
    }

    Vector3 GetRandomPosition()
    {
        float camHeight = mainCamera.orthographicSize;
        float camWidth = camHeight * mainCamera.aspect;

        Vector3 cameraPosition = mainCamera.transform.position;

        float spawnX = Random.Range(-1f, 1f); // -1f trái, 1f phải
        float spawnY = Random.Range(-1f, 1f); // -1f dưới, 1f trên

        if (Mathf.Abs(spawnX) > Mathf.Abs(spawnY))
        {
            spawnX = Mathf.Sign(spawnX) * (camWidth + 1f) + cameraPosition.x;
            spawnY = Random.Range(cameraPosition.y - camHeight + 1f, cameraPosition.y + camHeight - 1f);
        }
        else
        {
            spawnX = Random.Range(cameraPosition.x - camWidth + 1f, cameraPosition.x + camWidth - 1f);
            spawnY = Mathf.Sign(spawnY) * (camHeight + 1f) + cameraPosition.y;
        }

        return new Vector3(spawnX, spawnY, 0f);
    }
}