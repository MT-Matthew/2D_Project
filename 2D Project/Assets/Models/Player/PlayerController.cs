using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerController : MonoBehaviour
{
    [Header("Health Settings")]
    public CharacterScriptableObject character;
    public InformationScriptableObject information;
    public Image healthBar;
    public Image tempBar;
    public Character playerData;
    private Information playerInfo;

    [Header("Player Stats")]
    public float maxHealth;
    public float currentHealth;
    public float currentSpeed;
    public float currentDamage;
    public float currentDefend;
    public float currentPickup;
    public float currentHaste;
    public float currentCrit;
    public float currentLifeSteal;
    public float currentAbsorb;
    public int id;
    public int choosedID;

    [Header("Information Show")]
    public TextMeshProUGUI nameText;
    public Image faceIcon;

    [Header("Stats Show %")]
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI critText;
    public TextMeshProUGUI pickupText;
    public TextMeshProUGUI hasteText;

    private float statHealth;
    private float statSpeed;
    private float statDamage;
    private float statPickup;
    private float statHaste;
    private float statCrit;

    [Header("Inventory")]
    public int weaponIndex;
    public int itemIndex;

    bool canMove = true;
    InventoryManager inventory;
    Leveling level;
    Vector2 movementInput = Vector2.zero;
    public Vector2 direction;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator animator;
    GameManager manager;
    public bool Damageable = true;
    bool isAddStartWeapon = false;

    void Start()
    {
        choosedID = PlayerPrefs.GetInt("CharacterID", id);
        playerData = character.GetCharacter(choosedID);
        playerInfo = information.GetInformation(choosedID);

        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();

        currentHealth = playerData.MaxHealth;
        currentSpeed = playerData.Speed;
        currentDamage = playerData.Damage;
        currentDefend = playerData.Defend;
        currentPickup = playerData.Pickup;
        currentHaste = playerData.Haste;
        currentCrit = playerData.Crit;
        currentLifeSteal = playerData.LifeSteal;
        currentAbsorb = playerData.Absorb;

        maxHealth = currentHealth;

        nameText.text = playerData.Name;
        faceIcon.sprite = playerInfo.FaceIcon;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        level = GameObject.FindGameObjectWithTag("Manager").GetComponent<Leveling>();

        GetComponent<PlayerDamage>()._health = currentHealth;
        UpdateHealthBar(currentHealth);

        inventory = GetComponent<InventoryManager>();

        CountWeapon(playerData.StartWeapon, true);
        switch (playerData.ID)
        {
            case 0:
                animator.SetLayerWeight(animator.GetLayerIndex("Ame Layer"), 1);
                animator.SetLayerWeight(animator.GetLayerIndex("Gura Layer"), 0);
                break;
            case 1:
                animator.SetLayerWeight(animator.GetLayerIndex("Ame Layer"), 0);
                animator.SetLayerWeight(animator.GetLayerIndex("Gura Layer"), 1);
                break;
            default:
                break;
        }
    }

    void Update()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Horizontal", movementInput.x);
        animator.SetFloat("Vertical", movementInput.y);

        currentHealth = GetComponent<PlayerDamage>().Health;

        StatShow();
        if (currentHealth <= 0)
        {
            manager.GameOver();
        }
    }

    private void FixedUpdate()
    {
        if (canMove && movementInput != Vector2.zero)
        {
            direction = new Vector2(movementInput.x, movementInput.y).normalized;
            rb.AddForce(direction * currentSpeed * 1800 * Time.fixedDeltaTime);

            if (movementInput.x > 0)
            {
                spriteRenderer.transform.localScale = new Vector3(1, 1, 1);
                // spriteRenderer.flipX = false;
            }
            else if (movementInput.x < 0)
            {
                spriteRenderer.transform.localScale = new Vector3(-1, 1, 1);
                // spriteRenderer.flipX = true;
            }

            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }


    public void CountWeapon(GameObject weapon, bool isLevelUp)
    {
        if (weaponIndex >= inventory.weaponSlots.Count)
        {
            Debug.Log("Inventory Full");
            return;
        }
        GameObject update = Instantiate(weapon, transform.position, Quaternion.identity);
        update.transform.parent = transform;
        inventory.AddWeapon(weaponIndex, update.GetComponent<WeaponController>());

        if (!isAddStartWeapon)
        {
            inventory.StartWeapon[0].weaponUpgradeIndex = inventory.weaponUpgradeOptions.Count - 1;
            inventory.StartWeapon[0].initialWeapon = update.GetComponent<WeaponController>().weaponData.Prefab;
            inventory.StartWeapon[0].weaponData = update.GetComponent<WeaponController>().weaponData;
            isAddStartWeapon = true;
        }

        weaponIndex++;

        if (isLevelUp)
        {
            manager.EndLevelUp();
        }
        else
        {
            manager.EndLoot();
        }
    }

    public void CountItem(GameObject item, bool isLevelUp)
    {
        if (itemIndex >= inventory.itemSlots.Count)
        {
            Debug.Log("Inventory Full");
            return;
        }
        GameObject update = Instantiate(item, transform.position, Quaternion.identity);
        update.transform.parent = transform;
        inventory.AddItem(itemIndex, update.GetComponent<Item>());

        itemIndex++;

        if (isLevelUp)
        {
            manager.EndLevelUp();
        }
        else
        {
            manager.EndLoot();
        }

    }

    public void UpdateHealthBar(float healPoint)
    {
        healthBar.fillAmount = healPoint / maxHealth;
        StartCoroutine(StartTempBar(GetComponent<PlayerDamage>().Health));
    }

    public void GainHealth(float healPoint)
    {
        float tempHealth = healPoint + currentHealth;
        if (tempHealth > maxHealth)
        {
            // tempHealth = (tempHealth) - (tempHealth - maxHealth) - (currentHealth);
            // GetComponent<PlayerDamage>().Health += tempHealth;
            GetComponent<PlayerDamage>().Health = maxHealth;
        }
        else
        {
            GetComponent<PlayerDamage>().Health += healPoint;
        }

        UpdateHealthBar(GetComponent<PlayerDamage>().Health);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Mini Boss") || other.CompareTag("Boss"))
        {
            IDamageable damageableObject = gameObject.GetComponent<IDamageable>();

            if (damageableObject != null)
            {
                EnemyStat impact = other.GetComponent<EnemyStat>();
                if (impact == null) return;


                damageableObject.OnHit(impact.currentDamage);
                if (impact.isHarmless)
                {
                    Destroy(other.gameObject);
                    other.GetComponent<EnemyController>().killByPlayer = true;
                }

            }

            return;
        }

        Transform other2 = other.transform.parent;
        if (other2 == null) return;

        Transform other3 = other2.transform.parent;
        if (other3 == null) return;

        if (other3.CompareTag("Enemy") || other3.CompareTag("Mini Boss") || other3.CompareTag("Boss"))
        {
            IDamageable damageableObject = gameObject.GetComponent<IDamageable>();

            if (damageableObject != null)
            {
                EnemyStat impact = other3.GetComponent<EnemyStat>();
                if (impact == null) return;


                damageableObject.OnHit(impact.currentDamage);
                if (impact.isHarmless)
                {
                    Destroy(other3.gameObject);
                    other3.GetComponent<EnemyController>().killByPlayer = true;
                }

            }
        }


    }

    public IEnumerator LerpColor(Color startColor, Color endColor)
    {
        float elapsedTime = 0f;

        while (elapsedTime < 0.1f)
        {
            GetComponent<SpriteRenderer>().color = Color.Lerp(startColor, endColor, elapsedTime / 0.1f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        GetComponent<SpriteRenderer>().color = endColor;

        float elapsedTime2 = 0f;

        while (elapsedTime2 < 0.1f)
        {
            GetComponent<SpriteRenderer>().color = Color.Lerp(endColor, startColor, elapsedTime2 / 0.1f);
            elapsedTime2 += Time.deltaTime;
            yield return null;
        }

        GetComponent<SpriteRenderer>().color = startColor;
    }

    public void StatShow()
    {

        // statHealth = (playerData.MaxHealth != 0) ? (maxHealth / playerData.MaxHealth) * 100 : 100;
        statDamage = (playerData.Damage != 0) ? (currentDamage / playerData.Damage) * 100 : 100;
        statSpeed = (playerData.Speed != 0) ? (currentSpeed / playerData.Speed) * 100 : 100;
        // statPickup = (playerData.Pickup != 0) ? (currentPickup / playerData.Pickup) * 100 : 100;
        // statHaste = (playerData.Haste != 0) ? (currentHaste / playerData.Haste) * 100 : 100;
        // statCrit = (playerData.Crit != 0) ? (currentCrit / playerData.Crit) * 100 : 100;

        // statDamage = (currentDamage / playerData.Speed) * 100;
        // statSpeed = (currentSpeed / playerData.Speed) * 100;
        statPickup = currentPickup;
        statHaste = currentHaste;
        statCrit = currentCrit;

        healthText.text = (Math.Max(0, (int)currentHealth)).ToString() + " / " + ((int)maxHealth).ToString();
        speedText.text = ((int)statSpeed).ToString() + " %";
        damageText.text = ((int)statDamage).ToString() + " %";
        pickupText.text = ((int)statPickup).ToString() + " %";
        hasteText.text = ((int)statHaste).ToString() + " %";
        critText.text = ((int)statCrit).ToString() + " %";
    }

    IEnumerator StartTempBar(float healPoint)
    {
        float elapsedTime = 0f;
        float startIntensity = tempBar.fillAmount;

        while (elapsedTime < 1)
        {
            tempBar.fillAmount = Mathf.Lerp(startIntensity, (healPoint / maxHealth), (elapsedTime / 1));
            elapsedTime += Time.deltaTime;
            yield return null;
        }


        tempBar.fillAmount = healPoint / maxHealth;
    }

    void Remove()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

}