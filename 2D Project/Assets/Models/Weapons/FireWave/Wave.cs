using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
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
        if (other.CompareTag("Enemy") || other.CompareTag("Mini Boss") || other.CompareTag("Boss") && !markedObjects.Contains(other))
        {
            EnemyStat enemy = other.GetComponent<EnemyStat>();
            if (enemy != null)
            {
                if (enemy.fireEffect < 5)
                {
                    enemy.fireEffect += 1;
                    enemy.endBurnEffect = 0;
                    enemy.isOnFire = true;
                    markedObjects.Add(other);
                }
                else
                {
                    enemy.endBurnEffect = 0;
                    enemy.isOnFire = true;
                    markedObjects.Add(other);
                }
            }
        }
    }
}
