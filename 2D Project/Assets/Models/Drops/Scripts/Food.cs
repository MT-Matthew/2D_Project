using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public Vector3 foodPosition;

    private GameObject player;

    private float healValue;
    private bool collected = false;
    private bool canMove = true;
    private float speed = 4.5f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (foodPosition != null && canMove)
        {
            if (collected)
            {
                foodPosition = player.transform.position;
            }

            Vector3 direction = (foodPosition - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            if (Vector2.Distance(transform.position, foodPosition) <= 0.1f)
            {
                canMove = false;
                if (collected)
                {
                    GameObject.FindGameObjectWithTag("Heal").GetComponent<AudioSource>().Play();
                    Destroy(gameObject);
                    PlayerController playerController = player.GetComponent<PlayerController>();
                    healValue = playerController.maxHealth * 20 / 100;
                    playerController.GainHealth(healValue);
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
