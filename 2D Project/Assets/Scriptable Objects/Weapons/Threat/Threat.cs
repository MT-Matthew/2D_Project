using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Threat : MonoBehaviour
{
    public float maxRadius = 5.0f;
    public float currentRadius = 0.0f;
    public float speed = 2.0f;

    private List<Collider2D> markedObjects = new List<Collider2D>();
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        gameObject.transform.position = player.transform.position;
        currentRadius += speed * Time.deltaTime;
        if (currentRadius >= maxRadius)
        {
            Destroy(gameObject);
        }

        transform.localScale = Vector3.one * currentRadius * 2; // Đặt kích thước theo bán kính
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && !markedObjects.Contains(other))
        {
            EnemyStat enemy = other.GetComponent<EnemyStat>();
            if (enemy != null)
            {
                enemy.isFear = true;
                enemy.fearDuration = 2f;
                enemy.countFearTime = 0f;
            }
        }
    }
}
