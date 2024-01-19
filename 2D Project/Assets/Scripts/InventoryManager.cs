using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public List<WeaponController> weaponSlots = new List<WeaponController>(6);
    public int[] weaponLevels = new int[6];
    public List<Image> weaponUISlots = new List<Image>(6);
    public List<Image> weaponLevelSlots = new List<Image>(6);
    public List<TextMeshProUGUI> weaponTextSlots = new List<TextMeshProUGUI>(6);

    public List<Item> itemSlots = new List<Item>(6);
    public int[] itemLevels = new int[6];
    public List<Image> itemUISlots = new List<Image>(6);
    public List<Image> itemLevelSlots = new List<Image>(6);
    public List<TextMeshProUGUI> itemTextSlots = new List<TextMeshProUGUI>(6);


    [System.Serializable]
    public class WeaponUpgrade
    {
        public int weaponUpgradeIndex;
        public GameObject initialWeapon;
        public WeaponScriptableObject weaponData;
    }

    [System.Serializable]
    public class ItemUpgrade
    {
        public int itemUpgradeIndex;
        public GameObject initialItem;
        public ItemScriptableObject itemData;
    }

    [System.Serializable]
    public class UpgradeUI
    {
        public Text nameDisplay;
        public Text descriptionDisplay;
        public Image iconDisplay;
        public Button buttonDisplay;
    }

    public List<WeaponUpgrade> StartWeapon = new List<WeaponUpgrade>();
    public List<WeaponUpgrade> weaponUpgradeOptions = new List<WeaponUpgrade>();
    public List<ItemUpgrade> itemUpgradeOptions = new List<ItemUpgrade>();
    public List<UpgradeUI> upgradeUIOptions = new List<UpgradeUI>();

    PlayerController player;
    Counting counting;
    GameManager manager;

    public Button lootButton;
    public TextMeshProUGUI lootName;
    public Image lootIcon;

    bool isInvalid = true;
    bool canDisplay = true;
    bool finished = false;

    void Start()
    {
        weaponUpgradeOptions.AddRange(StartWeapon);

        player = GetComponent<PlayerController>();

        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        counting = GameObject.FindGameObjectWithTag("Manager").GetComponent<Counting>();
    }

    public void AddWeapon(int slotIndex, WeaponController weapon)
    {
        weaponSlots[slotIndex] = weapon;
        weaponLevels[slotIndex] = weapon.weaponData.Level;

        weaponUISlots[slotIndex].enabled = true;
        weaponUISlots[slotIndex].sprite = weapon.weaponData.Icon;
        weaponLevelSlots[slotIndex].enabled = true;
        weaponTextSlots[slotIndex].enabled = true;
        weaponTextSlots[slotIndex].text = weapon.weaponData.Level.ToString();
    }
    public void AddItem(int slotIndex, Item item)
    {
        itemSlots[slotIndex] = item;
        itemLevels[slotIndex] = item.itemData.Level;

        itemUISlots[slotIndex].enabled = true;
        itemUISlots[slotIndex].sprite = item.itemData.Icon;
        itemLevelSlots[slotIndex].enabled = true;
        itemTextSlots[slotIndex].enabled = true;
        itemTextSlots[slotIndex].text = item.itemData.Level.ToString();
    }



    public void LevelUpWeapon(int slotIndex, int upgradeIndex, bool isLevelUp)
    {
        if (weaponSlots.Count > slotIndex)
        {
            WeaponController weapon = weaponSlots[slotIndex];
            if (!weapon.weaponData.NextLevelPrefab)
            {
                Debug.LogError("No Next Level For: " + weapon.name);
                return;
            }
            GameObject upgradeWeapon = Instantiate(weapon.weaponData.NextLevelPrefab, transform.position, Quaternion.identity);
            upgradeWeapon.transform.SetParent(transform);
            AddWeapon(slotIndex, upgradeWeapon.GetComponent<WeaponController>());
            Destroy(weapon.gameObject);
            weaponLevels[slotIndex] = upgradeWeapon.GetComponent<WeaponController>().weaponData.Level;

            weaponUpgradeOptions[upgradeIndex].weaponData = upgradeWeapon.GetComponent<WeaponController>().weaponData;
        }

        if (isLevelUp)
        {
            manager.EndLevelUp();
        }
        else
        {
            manager.EndLoot();
        }

    }
    public void LevelUpItem(int slotIndex, int upgradeIndex, bool isLevelUp)
    {
        if (itemSlots.Count > slotIndex)
        {
            Item item = itemSlots[slotIndex];
            if (!item.itemData.NextLevelPrefab)
            {
                Debug.LogError("No Next Level For: " + item.name);
                return;
            }
            GameObject upgradeItem = Instantiate(item.itemData.NextLevelPrefab, transform.position, Quaternion.identity);
            upgradeItem.transform.SetParent(transform);
            AddItem(slotIndex, upgradeItem.GetComponent<Item>());
            Destroy(item.gameObject);
            itemLevels[slotIndex] = upgradeItem.GetComponent<Item>().itemData.Level;

            itemUpgradeOptions[upgradeIndex].itemData = upgradeItem.GetComponent<Item>().itemData;
        }

        if (isLevelUp)
        {
            manager.EndLevelUp();
        }
        else
        {
            manager.EndLoot();
        }
    }

    void ApplyUpgradeOptions()
    {
        List<WeaponUpgrade> availableWeaponUpgrades = new List<WeaponUpgrade>(weaponUpgradeOptions);
        List<ItemUpgrade> availableItemUpgrades = new List<ItemUpgrade>(itemUpgradeOptions);

        foreach (var upgradeOptions in upgradeUIOptions)
        {
            int upgradeType;
            canDisplay = false;
            // bool removed = false;
            while (!canDisplay)
            {
                if (availableWeaponUpgrades.Count == 0)
                {
                    upgradeType = 2;
                }
                else if (availableItemUpgrades.Count == 0)
                {
                    upgradeType = 1;
                }
                else
                {
                    upgradeType = Random.Range(1, 3);
                }

                if (availableWeaponUpgrades.Count == 0 && availableItemUpgrades.Count == 0)
                {
                    upgradeType = 3;
                }

                if (upgradeType == 1)
                {
                    WeaponUpgrade chosenWeaponUpgrade = availableWeaponUpgrades[Random.Range(0, availableWeaponUpgrades.Count)];
                    availableWeaponUpgrades.Remove(chosenWeaponUpgrade);

                    if (chosenWeaponUpgrade != null)
                    {
                        EnableUpgradeUI(upgradeOptions);

                        bool newWeapon = true;
                        for (int i = 0; i < weaponSlots.Count; i++)
                        {
                            if (weaponSlots[i] != null && weaponSlots[i].weaponData == chosenWeaponUpgrade.weaponData)
                            {
                                newWeapon = false;
                                if (!newWeapon) // if that weapon already have
                                {
                                    if (!chosenWeaponUpgrade.weaponData.NextLevelPrefab) // If weapon max level
                                    {
                                        canDisplay = false;
                                        break;
                                    }
                                    upgradeOptions.buttonDisplay.onClick.AddListener(() => LevelUpWeapon(i, chosenWeaponUpgrade.weaponUpgradeIndex, true));
                                    upgradeOptions.descriptionDisplay.text = chosenWeaponUpgrade.weaponData.NextLevelPrefab.GetComponent<WeaponController>().weaponData.Description;
                                    upgradeOptions.nameDisplay.text = chosenWeaponUpgrade.weaponData.NextLevelPrefab.GetComponent<WeaponController>().weaponData.Name;
                                    canDisplay = true;
                                    upgradeOptions.iconDisplay.sprite = chosenWeaponUpgrade.weaponData.NextLevelPrefab.GetComponent<WeaponController>().weaponData.Icon;
                                }
                                break;
                            }
                            else
                            {
                                newWeapon = true;
                            }
                        }
                        if (newWeapon) // if that weapon don't have (Spawn a new weapon)
                        {
                            upgradeOptions.buttonDisplay.onClick.AddListener(() => player.CountWeapon(chosenWeaponUpgrade.weaponData.Prefab, true));
                            upgradeOptions.descriptionDisplay.text = chosenWeaponUpgrade.weaponData.Description;
                            upgradeOptions.nameDisplay.text = chosenWeaponUpgrade.weaponData.Name;
                            canDisplay = true;
                            if (player.weaponIndex >= weaponSlots.Count)
                            {
                                canDisplay = false;
                            }
                            upgradeOptions.iconDisplay.sprite = chosenWeaponUpgrade.weaponData.Icon;
                        }
                    }
                }
                else if (upgradeType == 2)
                {
                    ItemUpgrade chosenItemUpgrade = availableItemUpgrades[Random.Range(0, availableItemUpgrades.Count)];
                    availableItemUpgrades.Remove(chosenItemUpgrade);

                    if (chosenItemUpgrade != null)
                    {
                        EnableUpgradeUI(upgradeOptions);
                        bool newItem = true;
                        for (int i = 0; i < itemSlots.Count; i++)
                        {
                            if (itemSlots[i] != null && itemSlots[i].itemData == chosenItemUpgrade.itemData)
                            {
                                newItem = false;
                                if (!newItem) // if that item already have
                                {
                                    if (!chosenItemUpgrade.itemData.NextLevelPrefab) // If item max level
                                    {
                                        canDisplay = false;
                                        break;
                                    }
                                    upgradeOptions.buttonDisplay.onClick.AddListener(() => LevelUpItem(i, chosenItemUpgrade.itemUpgradeIndex, true));
                                    upgradeOptions.descriptionDisplay.text = chosenItemUpgrade.itemData.NextLevelPrefab.GetComponent<Item>().itemData.Description;
                                    upgradeOptions.nameDisplay.text = chosenItemUpgrade.itemData.NextLevelPrefab.GetComponent<Item>().itemData.Name;
                                    canDisplay = true;
                                    upgradeOptions.iconDisplay.sprite = chosenItemUpgrade.itemData.NextLevelPrefab.GetComponent<Item>().itemData.Icon;
                                }
                                break;
                            }
                            else
                            {
                                newItem = true;
                            }
                        }
                        if (newItem) // if that item don't have (Spawn a new item)
                        {
                            upgradeOptions.buttonDisplay.onClick.AddListener(() => player.CountItem(chosenItemUpgrade.itemData.Prefab, true));
                            upgradeOptions.descriptionDisplay.text = chosenItemUpgrade.itemData.Description;
                            upgradeOptions.nameDisplay.text = chosenItemUpgrade.itemData.Name;
                            canDisplay = true;
                            if (player.itemIndex >= itemSlots.Count)
                            {
                                canDisplay = false;
                            }
                            upgradeOptions.iconDisplay.sprite = chosenItemUpgrade.itemData.Icon;
                        }
                    }
                }
                else if (upgradeType == 3)
                {
                    EnableUpgradeUI(upgradeOptions);
                    upgradeOptions.buttonDisplay.onClick.RemoveAllListeners();
                    upgradeOptions.buttonDisplay.onClick.AddListener(() => IncreaseGold());
                    upgradeOptions.descriptionDisplay.text = "Thêm 100 Vàng vào túi";
                    upgradeOptions.nameDisplay.text = "Đại Gia";
                    upgradeOptions.iconDisplay.sprite = goldIcon;
                    canDisplay = true;
                }
            }
        }
        finished = true;
    }

    void FindLootOption()
    {
        List<WeaponUpgrade> availableWeaponUpgrades = new List<WeaponUpgrade>(weaponUpgradeOptions);
        List<ItemUpgrade> availableItemUpgrades = new List<ItemUpgrade>(itemUpgradeOptions);

        int upgradeType;

        isInvalid = true;
        while (isInvalid)
        {
            if (availableWeaponUpgrades.Count == 0)
            {
                upgradeType = 2;
            }
            else if (availableItemUpgrades.Count == 0)
            {
                upgradeType = 1;
            }
            else
            {
                upgradeType = Random.Range(1, 3);
            }

            if (availableWeaponUpgrades.Count == 0 && availableItemUpgrades.Count == 0)
            {
                upgradeType = 3;
            }

            if (upgradeType == 1)
            {
                WeaponUpgrade chosenWeaponUpgrade = availableWeaponUpgrades[Random.Range(0, availableWeaponUpgrades.Count)];
                availableWeaponUpgrades.Remove(chosenWeaponUpgrade);

                if (chosenWeaponUpgrade != null)
                {
                    bool newWeapon = false;
                    for (int i = 0; i < weaponSlots.Count; i++)
                    {
                        if (weaponSlots[i] != null && weaponSlots[i].weaponData == chosenWeaponUpgrade.weaponData)
                        {
                            newWeapon = false;
                            if (!newWeapon) // if that weapon already have
                            {
                                if (!chosenWeaponUpgrade.weaponData.NextLevelPrefab) // If weapon max level
                                {
                                    isInvalid = true;
                                    break;
                                }
                                lootButton.onClick.AddListener(() => LevelUpWeapon(i, chosenWeaponUpgrade.weaponUpgradeIndex, false));
                                lootName.text = chosenWeaponUpgrade.weaponData.NextLevelPrefab.GetComponent<WeaponController>().weaponData.Name;
                                isInvalid = false;
                                lootIcon.sprite = chosenWeaponUpgrade.weaponData.NextLevelPrefab.GetComponent<WeaponController>().weaponData.Icon;
                            }
                            break;
                        }
                        else
                        {
                            newWeapon = true;
                        }
                    }
                    if (newWeapon) // if that weapon don't have (Spawn a new weapon)
                    {
                        lootButton.onClick.AddListener(() => player.CountWeapon(chosenWeaponUpgrade.weaponData.Prefab, false));
                        lootName.text = chosenWeaponUpgrade.weaponData.Name;
                        isInvalid = false;
                        if (player.weaponIndex >= weaponSlots.Count - 1)
                        {
                            isInvalid = true;
                        }
                        lootIcon.sprite = chosenWeaponUpgrade.weaponData.Icon;
                    }
                }
            }
            else if (upgradeType == 2)
            {
                ItemUpgrade chosenItemUpgrade = availableItemUpgrades[Random.Range(0, availableItemUpgrades.Count)];
                availableItemUpgrades.Remove(chosenItemUpgrade);

                if (chosenItemUpgrade != null)
                {
                    bool newItem = false;
                    for (int i = 0; i < itemSlots.Count; i++)
                    {
                        if (itemSlots[i] != null && itemSlots[i].itemData == chosenItemUpgrade.itemData)
                        {
                            newItem = false;
                            if (!newItem) // if that item already have
                            {
                                if (!chosenItemUpgrade.itemData.NextLevelPrefab) // If item max level
                                {
                                    isInvalid = true;
                                    break;
                                }
                                lootButton.onClick.AddListener(() => LevelUpItem(i, chosenItemUpgrade.itemUpgradeIndex, false));
                                lootName.text = chosenItemUpgrade.itemData.NextLevelPrefab.GetComponent<Item>().itemData.Name;
                                isInvalid = false;
                                lootIcon.sprite = chosenItemUpgrade.itemData.NextLevelPrefab.GetComponent<Item>().itemData.Icon;
                            }
                            break;
                        }
                        else
                        {
                            newItem = true;
                        }
                    }
                    if (newItem) // if that item don't have (Spawn a new item)
                    {
                        lootButton.onClick.AddListener(() => player.CountItem(chosenItemUpgrade.itemData.Prefab, false));
                        lootName.text = chosenItemUpgrade.itemData.Name;
                        isInvalid = false;
                        if (player.itemIndex >= itemSlots.Count - 1)
                        {
                            isInvalid = true;
                        }
                        lootIcon.sprite = chosenItemUpgrade.itemData.Icon;
                    }
                }

            }
            else if (upgradeType == 3)
            {
                lootButton.onClick.RemoveAllListeners();
                lootButton.onClick.AddListener(() => IncreaseGoldLoot());
                lootName.text = "Đại Gia";
                lootIcon.sprite = goldIcon;
                isInvalid = false;
            }
        }

    }

    void LootBox()
    {
        lootButton.onClick.RemoveAllListeners();

        FindLootOption();
        manager.StartLevelUp(false);
    }

    void RemoveUpgradeOptions()
    {
        foreach (var upgradeOptions in upgradeUIOptions)
        {
            upgradeOptions.buttonDisplay.onClick.RemoveAllListeners();
            DisableUpgradeUI(upgradeOptions);
        }
    }

    void RemoveAndApplyUpgrades()
    {
        RemoveUpgradeOptions();

        finished = false;
        ApplyUpgradeOptions();
        manager.StartLevelUp(finished);
    }

    void DisableUpgradeUI(UpgradeUI ui)
    {
        ui.nameDisplay.transform.parent.gameObject.SetActive(false);
    }

    void EnableUpgradeUI(UpgradeUI ui)
    {
        ui.nameDisplay.transform.parent.gameObject.SetActive(true);
    }

    void IncreaseGold()
    {
        counting.goldCount += 100;
        Debug.Log("You Increased 100G");
        manager.EndLevelUp();
    }

    void IncreaseGoldLoot()
    {
        counting.goldCount += 100;
        Debug.Log("You Increased 100G");
        manager.EndLoot();
    }

    public Sprite goldIcon;

}
