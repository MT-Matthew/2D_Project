using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public Vector3 coinPosition;

    private GameObject player;
    private Counting counting;
    private GameObject sound;

    private float coinValue;
    private bool collected = false;
    private bool canMove = true;
    private float speed = 4.5f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        counting = GameObject.FindGameObjectWithTag("Manager").GetComponent<Counting>();
        sound = GameObject.FindGameObjectWithTag("Coin");
    }

    void Update()
    {
        if (coinPosition != null && canMove)
        {
            if (collected)
            {
                coinPosition = player.transform.position;
            }

            Vector3 direction = (coinPosition - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            if (Vector2.Distance(transform.position, coinPosition) <= 0.1f)
            {
                canMove = false;
                if (collected)
                {
                    sound.GetComponent<AudioSource>().Play();
                    Destroy(gameObject);
                    counting.goldCount++;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Magnet"))
        {
            canMove = true;
            collected = true;
            speed = 6f;
        }
    }
}
