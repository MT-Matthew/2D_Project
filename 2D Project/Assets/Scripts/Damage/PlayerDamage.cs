using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour, IDamageable
{
    public bool canTurnInvincible;
    public float invincibilityTime = 0.5f;
    private float invincibleTimeElapsed = 0f;

    // Animator animator;
    Rigidbody2D rb;
    Collider2D physicsCollider;
    PlayerController player;

    public bool _targetable = true;
    public float _health;
    public bool _invincible;

    // bool isAlive = true;
    public bool Targetable
    {
        get { return _targetable; }
        set
        {
            _targetable = value;
            rb.simulated = false;
            physicsCollider.enabled = value;
        }
    }

    public float Health
    {
        set
        {
            if (value < _health)
            {
                // animator.SetTrigger("hit");
            }

            _health = value;

            if (_health <= 0)
            {
                // animator.SetBool("isAlive", false);
                // Targetable = false;

                GetComponent<Animator>().SetBool("isDead", true);
                manager.SendMessage("GameOver");
            }
        }
        get
        {
            return _health;
        }
    }

    public bool Invincible
    {
        get { return _invincible; }
        set
        {
            _invincible = value;
            if (_invincible == true)
            {
                invincibleTimeElapsed = 0f;
            }
        }
    }

    public void OnHit(float damage, Vector2 knockback)
    {
        // if (!Invincible)
        // {
        //     Health -= damage;
        //     rb.AddForce(knockback, ForceMode2D.Impulse);
        //     Debug.Log("Player get hit: " + damage);
        //     if (canTurnInvincible)
        //     {
        //         Invincible = true;
        //     }
        // }
    }

    public void OnHit(float damage)
    {
        if (!Invincible)
        {
            if (player.Damageable)
            {
                if (damage > 0)
                {
                    GameObject.FindGameObjectWithTag("Hurt").GetComponent<AudioSource>().Play();
                    StartCoroutine(player.LerpColor(new Color(1f, 1f, 1f, 1f), new Color(1f, 75f / 255f, 75f / 255f, 1f)));
                }

                if (GameObject.FindGameObjectWithTag("Manager").GetComponent<Timer>().isNight)
                {
                    damage *= 1.5f;
                }

                Health -= ((damage / 100f) * (100f - player.currentDefend));
                player.UpdateHealthBar(Health);

                if (canTurnInvincible)
                {
                    Invincible = true;
                }
            }
        }
    }

    public void Remove()
    {
        // Destroy(gameObject);
    }

    private GameObject manager;
    public void Start()
    {
        // animator = GetComponent<Animator>();
        // animator.SetBool("isAlive", isAlive);
        rb = GetComponent<Rigidbody2D>();
        physicsCollider = GetComponent<Collider2D>();
        player = GetComponent<PlayerController>();
        manager = GameObject.FindGameObjectWithTag("Manager");
    }

    public void FixedUpdate()
    {
        if (Invincible)
        {
            invincibleTimeElapsed += Time.deltaTime;
            if (invincibleTimeElapsed > invincibilityTime)
            {
                Invincible = false;
            }
        }
    }
}
