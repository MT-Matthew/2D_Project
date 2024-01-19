using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    public SkillScriptableObject skillScript;

    GameManager manager;


    public Image skillBar;

    public float skillCount;

    public bool isActive = false;
    public bool isReady = false;

    GameObject destroyObject;
    public float count = 0;

    public int id;
    public int choosedID;

    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        skillCount = 0f;
        choosedID = PlayerPrefs.GetInt("CharacterID", id);
    }

    void FixedUpdate()
    {
        if (isActive)
        {
            count += Time.fixedDeltaTime;

            if (count >= skillScript.skills[choosedID].duration)
            {
                Destroy(destroyObject);
                count = 0f;
            }
        }
    }


    void Update()
    {
        if (!isReady)
        {
            UpdateSkillBar();
        }
        else if (isReady)
        {
            if (Input.GetMouseButtonDown(0) && manager.currentState == GameManager.GameState.GamePlay)
            {
                skillCount = 0f;
                skillBar.fillAmount = skillCount / (skillScript.skills[choosedID].cooldown * 60f);
                if (!isActive)
                {
                    isActive = true;
                    GameObject newSkill = Instantiate(skillScript.skills[choosedID].skillPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                    destroyObject = newSkill;
                    newSkill.transform.parent = gameObject.transform;

                    // Destroy(newSkill, skillScript.skills[id].duration);
                }
            }
        }
    }

    void UpdateSkillBar()
    {
        skillCount += Time.deltaTime;
        skillBar.fillAmount = skillCount / (skillScript.skills[choosedID].cooldown * 60f);

        if (skillCount >= (skillScript.skills[choosedID].cooldown * 60f))
        {
            isReady = true;
        }
    }
}
