using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : MonoBehaviour
{
    public int expValue;
    public Vector3 expPosition;

    private SpriteRenderer spriteRenderer;
    private GameObject player;
    private GameObject sound;

    public bool canMove = true;
    private bool collected = false;

    private float speed = 4.5f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        CheckExpValue();

        player = GameObject.FindGameObjectWithTag("Player");
        sound = GameObject.FindGameObjectWithTag("Exp-Collect");
    }

    void Update()
    {
        if (expPosition != null && canMove)
        {
            if (collected)
            {
                expPosition = player.transform.position;
            }

            Vector3 direction = (expPosition - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            if (Vector2.Distance(transform.position, expPosition) <= 0.1f)
            {
                canMove = false;
                if (collected)
                {
                    sound.GetComponent<AudioSource>().Play();
                    Destroy(gameObject);
                    Leveling playerLeveling = GameObject.FindGameObjectWithTag("Manager").GetComponent<Leveling>();
                    playerLeveling.GainExperience(expValue);
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


    void CheckExpValue()
    {
        if (expValue >= 1 && expValue <= 10)
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        }
        if (expValue >= 11 && expValue <= 19)
        {
            spriteRenderer.color = new(0f, 1f, 61f / 255f, 1f);
        }
        if (expValue >= 20 && expValue <= 49)
        {
            spriteRenderer.color = new Color(1f, 1f, 0f, 1f);
        }
        if (expValue >= 50 && expValue <= 99)
        {
            spriteRenderer.color = new Color(1f, 123f / 255f, 0f, 1f);
        }
        if (expValue >= 100 && expValue <= 199)
        {
            spriteRenderer.color = new Color(1f, 0f, 116f / 255f, 1f);
        }
        if (expValue >= 200)
        {
            spriteRenderer.color = new Color(1f, 0f, 0f, 1f);
        }
    }
}