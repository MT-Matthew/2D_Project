using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    public EnemyScriptableObject enemyData;

    public bool isFreeze = false;
    public bool isOnFire = false;
    public bool isFear = false;

    public float currentHealth;
    public float currentDamage;
    public float currentSpeed;

    public float freezeDuration;
    public float countFreezeTime;

    public float fireEffect;
    public float countBurnTime;
    public float endBurnEffect;

    public float countFearTime;
    public float fearDuration;

    public bool canMove = true;

    private Transform player;
    private SpriteRenderer spriteRenderer;

    private float a;
    private float b;

    public Vector3 targetPosition;
    public float eventMoveSpeed;

    float startScale;
    public bool isHarmless;

    void Start()
    {
        isHarmless = false;
        a = GameObject.FindGameObjectWithTag("Manager").GetComponent<Timer>().a;
        b = GameObject.FindGameObjectWithTag("Manager").GetComponent<Timer>().b;

        if (a < 0)
        {
            a = 0;
        }

        currentDamage = Mathf.Pow(enemyData.Damage + 2f * a, 1f + b / 25f);
        currentSpeed = Mathf.Pow(enemyData.Speed + 0.12f * a, 1f + b / 25f);
        currentHealth = Mathf.Pow(enemyData.MaxHealth + enemyData.MaxHealth * 0.05f * a, 1f + b / 50f);

        if (GetComponent<EnemyController>().eventEnemy)
        {
            GetComponent<DamageableCharacter>().Health = currentHealth * 5f;
        }
        else
        {
            GetComponent<DamageableCharacter>().Health = currentHealth;
        }

        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        startScale = spriteRenderer.transform.localScale.x;
    }

    void FixedUpdate()
    {
        if (player != null && canMove)
        {
            if (!GetComponent<EnemyController>().eventEnemy)
            {
                Vector3 directionToPlayer = (player.position - transform.position).normalized;
                targetPosition = transform.position + (directionToPlayer * currentSpeed * Time.deltaTime);
                transform.position = targetPosition;
            }
            else
            {
                Vector3 direction = (targetPosition - transform.position).normalized;
                float distance = eventMoveSpeed * Time.deltaTime;
                transform.Translate(direction * distance);
            }


            if (transform.position.x <= player.position.x)
            {
                // spriteRenderer.flipX = false;
                if (isFear)
                {
                    spriteRenderer.transform.localScale = new Vector3(-startScale, startScale, 1);
                }
                else
                {
                    spriteRenderer.transform.localScale = new Vector3(startScale, startScale, 1);
                }
            }
            else
            {
                // spriteRenderer.flipX = true;
                if (isFear)
                {
                    spriteRenderer.transform.localScale = new Vector3(startScale, startScale, 1);
                }
                else
                {
                    spriteRenderer.transform.localScale = new Vector3(-startScale, startScale, 1);
                }
            }
        }
    }

    void Update()
    {


        if (isFreeze)
        {
            countFreezeTime += Time.deltaTime;
            Freeze();
            if (countFreezeTime >= freezeDuration)
            {
                EndFreeze();
            }
        }

        if (isFear)
        {
            transform.Find("fear").gameObject.SetActive(true);

            countFearTime += Time.deltaTime;
            Fear();
            if (countFearTime >= fearDuration)
            {
                EndFear();
                transform.Find("fear").gameObject.SetActive(false);
            }
        }

        if (currentDamage <= 0)
        {
            spriteRenderer.color = new Color(0, 255, 255, 255);
            isHarmless = true;
            // StartCoroutine(RemoveHarmless());
        }
        else
        {
            isHarmless = false;
        }

        if (isOnFire)
        {
            transform.Find("fire").gameObject.SetActive(true);

            countBurnTime += Time.deltaTime;
            endBurnEffect += Time.deltaTime;
            if (countBurnTime >= 3)
            {
                IDamageable damageableObject = gameObject.GetComponent<IDamageable>();
                if (damageableObject != null)
                {
                    damageableObject.OnHit((4f * fireEffect) + (GetComponent<DamageableCharacter>().Health * 2 / 100));
                    countBurnTime = 0;
                }
            }
            if (endBurnEffect >= 5)
            {
                isOnFire = false;
                endBurnEffect = 0;
            }
        }
        else
        {
            transform.Find("fire").gameObject.SetActive(false);
        }
    }

    public void Freeze()
    {
        canMove = false;
        // currentSpeed = 0;
        spriteRenderer.color = new Color(0, 0, 0, 255);
    }

    public void EndFreeze()
    {
        isFreeze = false;
        // currentSpeed = enemyData.Speed;
        canMove = true;
        spriteRenderer.color = new Color(255, 255, 255, 255);
    }

    public void Fear()
    {
        currentSpeed = -(enemyData.Speed * 0.3f);
    }

    public void EndFear()
    {
        currentSpeed = enemyData.Speed;
        isFear = false;
        countFearTime = 0f;
    }

    // private IEnumerator RemoveHarmless()
    // {
    //     yield return new WaitForSeconds(3f);
    //     GetComponent<DamageableCharacter>().Kill();
    // }
}