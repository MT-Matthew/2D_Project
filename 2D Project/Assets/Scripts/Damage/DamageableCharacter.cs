using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DamageableCharacter : MonoBehaviour, IDamageable, DropItems
{
    public bool disableSimulation = false;

    public DropScriptableObject drop;
    private GameObject expPrefab;
    private GameObject coinPrefab;
    private GameObject foodPrefab;
    private GameObject lootBox;
    private GameObject popUpPrefab;

    private GameObject soundBox;
    private GameObject soundFood;

    Animator animator;
    Rigidbody2D rb;
    Collider2D physicsCollider;

    public bool _targetable = true;
    public float _health;
    public bool _invincible = false;

    PlayerController playerController;
    float playerCrit;
    float playerLifeSteal;
    float playerHealth;
    float playerMaxHealth;
    float playerAbsorb;

    Vector3 dropPosition;
    float randomAngle;
    float randomDistance;
    int randomNum;


    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        physicsCollider = GetComponent<Collider2D>();

        expPrefab = drop.dropList[0].dropPrefab;
        coinPrefab = drop.dropList[1].dropPrefab;
        foodPrefab = drop.dropList[2].dropPrefab;
        lootBox = drop.dropList[3].dropPrefab;
        popUpPrefab = drop.dropList[4].dropPrefab;
    }

    void Update()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerCrit = playerController.currentCrit;
        playerLifeSteal = playerController.currentLifeSteal;
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDamage>().Health;
        playerMaxHealth = playerController.maxHealth;
        playerAbsorb = playerController.currentAbsorb;
    }

    // bool isAlive = true;
    public bool Targetable
    {
        get { return _targetable; }
        set
        {
            _targetable = value;
            if (disableSimulation)
            { rb.simulated = false; }
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
                GetComponent<EnemyController>().killByPlayer = true;
                Absorb();
                // Remove();
                Kill();

                DropOnDestroy();
                Targetable = false;
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
        }
    }


    public void OnHit(float damage, Vector2 knockback)
    {
        if (!Invincible && !GetComponent<EnemyStat>().isHarmless)
        {

            bool isCrit;
            randomNum = UnityEngine.Random.Range(1, 100);

            if (randomNum <= playerCrit)
            {
                damage *= 2;
                isCrit = true;
                Health -= damage;
                rb.AddForce(knockback, ForceMode2D.Impulse);
            }
            else
            {
                isCrit = false;
                Health -= damage;
                rb.AddForce(knockback, ForceMode2D.Impulse);
            }

            LifeStealing(damage);

            PopUp(damage, isCrit);
        }
    }

    public void OnHit(float damage)
    {
        if (!Invincible && !GetComponent<EnemyStat>().isHarmless)
        {
            bool isCrit;
            randomNum = UnityEngine.Random.Range(1, 100);
            if (randomNum <= playerCrit)
            {
                damage *= 2;
                isCrit = true;
                Health -= damage;
            }
            else
            {
                isCrit = false;
                Health -= damage;
            }

            LifeStealing(damage);
            PopUp(damage, isCrit);
        }
    }

    public void Remove()
    {
        Destroy(gameObject);
    }

    public void Kill()
    {
        Vector3 tempPosition = transform.position;
        if (transform.position.x <= playerController.transform.position.x)
        {
            StartCoroutine(SlowKill(new Color(1f, 1f, 1f, 0f), new Vector3(tempPosition.x - 1f, tempPosition.y + 0.5f, 1f)));
        }
        else
        {
            StartCoroutine(SlowKill(new Color(1f, 1f, 1f, 0f), new Vector3(tempPosition.x + 1f, tempPosition.y + 0.5f, 1f)));
        }
    }

    IEnumerator SlowKill(Color endColor, Vector3 endPosition)
    {
        GetComponent<EnemyStat>().currentDamage = 0f;
        GetComponent<EnemyStat>().canMove = false;
        float elapsedTime = 0f;
        Color startColor = GetComponent<SpriteRenderer>().color;
        Vector3 startPosition = transform.position;

        while (elapsedTime < 0.5f)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / 0.5f);
            GetComponent<SpriteRenderer>().color = Color.Lerp(startColor, endColor, elapsedTime / 0.5f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        GetComponent<SpriteRenderer>().color = endColor;
        transform.position = endPosition;

        Destroy(gameObject);
    }



    void DropOnDestroy()
    {
        DropExp();
        DropBox();
        DropHealth();
        DropCoin();
    }

    void DropExp()
    {
        int objectExpValue = GetComponent<EnemyStat>().enemyData.ExpValue;

        if (objectExpValue > 200)
        {
            int numType6 = objectExpValue / 200;
            for (int i = 0; i < numType6; i++)
            {
                randomAngle = UnityEngine.Random.Range(0f, 360f);
                randomDistance = UnityEngine.Random.Range(0f, 2f);
                dropPosition = transform.position + Quaternion.Euler(0f, 0f, randomAngle) * Vector3.right * randomDistance;

                GameObject exp = Instantiate(expPrefab, transform.position, Quaternion.identity);
                exp.GetComponent<Exp>().expValue = 200;
                exp.GetComponent<Exp>().expPosition = dropPosition;
            }
            randomAngle = UnityEngine.Random.Range(0f, 360f);
            randomDistance = UnityEngine.Random.Range(0f, 2f);
            dropPosition = transform.position + Quaternion.Euler(0f, 0f, randomAngle) * Vector3.right * randomDistance;

            GameObject exp2 = Instantiate(expPrefab, transform.position, Quaternion.identity);
            exp2.GetComponent<Exp>().expValue = objectExpValue % 200;
            exp2.GetComponent<Exp>().expPosition = dropPosition;
        }
        else
        {
            randomAngle = UnityEngine.Random.Range(0f, 360f);
            dropPosition = transform.position + Quaternion.Euler(0f, 0f, randomAngle) * Vector3.right * 1;

            GameObject exp = Instantiate(expPrefab, transform.position, Quaternion.identity);
            exp.GetComponent<Exp>().expValue = objectExpValue;
            exp.GetComponent<Exp>().expPosition = dropPosition;
        }
    }

    void DropBox()
    {
        if (gameObject.CompareTag("Mini Boss") || gameObject.CompareTag("Boss"))
        {
            if (lootBox == null) return;

            GameObject box = Instantiate(lootBox, transform.position, Quaternion.identity);
        }
    }

    void DropHealth()
    {
        randomNum = UnityEngine.Random.Range(1, 201);
        if (randomNum == 1)
        {
            randomAngle = UnityEngine.Random.Range(0f, 360f);
            dropPosition = transform.position + Quaternion.Euler(0f, 0f, randomAngle) * Vector3.right * 1;
            GameObject food = Instantiate(foodPrefab, transform.position, Quaternion.identity);
            food.GetComponent<Food>().foodPosition = dropPosition;
        }
    }

    void DropCoin()
    {
        randomNum = UnityEngine.Random.Range(1, 90);
        if (randomNum == 1)
        {
            randomAngle = UnityEngine.Random.Range(0f, 360f);
            dropPosition = transform.position + Quaternion.Euler(0f, 0f, randomAngle) * Vector3.right * 1;
            GameObject coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
            coin.GetComponent<Coin>().coinPosition = dropPosition;
        }
    }

    void PopUp(float damage, bool isCrit)
    {
        GameObject popUpText = Instantiate(popUpPrefab, transform.position, Quaternion.identity);
        popUpText.GetComponent<TextMeshPro>().text = Math.Round(damage).ToString();
        if (isCrit)
        {
            popUpText.GetComponent<TextMeshPro>().color = new Color(1f, 50f / 255f, 0f, 1f);
        }
    }

    void LifeStealing(float damage)
    {

        float healPoint = (damage * (playerLifeSteal / 100));
        if ((healPoint + playerHealth) > playerMaxHealth)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDamage>().Health = playerMaxHealth;
            playerController.UpdateHealthBar(playerMaxHealth);
        }
        else
        {
            playerController.GainHealth(healPoint);
        }
    }
    void Absorb()
    {
        randomNum = UnityEngine.Random.Range(1, 100);
        if (randomNum <= playerAbsorb)
            if ((20f + playerHealth) > playerMaxHealth)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDamage>().Health = playerMaxHealth;
                playerController.UpdateHealthBar(playerMaxHealth);
            }
            else
            {
                playerController.GainHealth(20f);
            }
    }
}