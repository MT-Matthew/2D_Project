using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float chanceToAffect = 1f;

    public bool isLeft = false;
    public bool isRight = false;

    public GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (isLeft)
        {
            gameObject.transform.position = player.transform.position + Vector3.left * 10;
        }
        else if (isRight)
        {
            gameObject.transform.position = player.transform.position + Vector3.right * 10;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Transform other2 = other.transform.parent;
        if (other2 == null) return;

        Transform other3 = other2.transform.parent;
        if (other3 == null) return;

        if (other3.CompareTag("Enemy"))
        {
            float randomValue = Random.Range(1, 11);

            if (randomValue <= chanceToAffect)
            {
                EnemyStat impact = other3.GetComponent<EnemyStat>();
                if (impact == null) return;

                if (!impact.isHarmless)
                {
                    impact.currentDamage = -3;
                    GameObject.FindGameObjectWithTag("Manager").GetComponent<EnemySpawner>().currentEnemyCount--;
                    Destroy(other3.gameObject, 3f);
                }
            }
        }
    }
}
