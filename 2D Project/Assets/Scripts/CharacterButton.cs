using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CharacterButton : MonoBehaviour
{
    public InformationScriptableObject information;
    public CharacterScriptableObject character;

    public int id;

    private bool isHover;

    private Information informationData;
    private Character characterData;

    private SelectManager selectManager;



    void Start()
    {
        characterData = character.GetCharacter(id);
        informationData = information.GetInformation(id);

        selectManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<SelectManager>();

        if (id == 0)
        {
            OnMouseHover();
        }

    }

    public void OnMouseHover()
    {
        GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>().Play();

        selectManager.imageLayer.GetComponent<RectTransform>().anchoredPosition = transform.parent.GetComponent<RectTransform>().anchoredPosition;

        selectManager.portraitLeft.GetComponent<SpriteRenderer>().sprite = informationData.Portrait;
        selectManager.portraitRight.GetComponent<SpriteRenderer>().sprite = informationData.Portrait;

        selectManager.characterName.text = characterData.Name;

        selectManager.attackIcon.sprite = informationData.AttackIcon;
        selectManager.attackName.text = informationData.AttackName;
        selectManager.attackDescription.text = informationData.AttackDescription;

        selectManager.skillIcon.sprite = informationData.SkillIcon;
        selectManager.skillName.text = informationData.SkillName;
        selectManager.skillDescription.text = informationData.SkillDescription;

        UpdateStat();
        UpdatePreview();
    }

    public void UpdateStat()
    {
        selectManager.healthBar.fillAmount = characterData.MaxHealth / 100;
        selectManager.damageBar.fillAmount = characterData.Damage / 2;
        selectManager.speedBar.fillAmount = characterData.Speed / 1.6f;
        selectManager.critBar.fillAmount = characterData.Crit / 20;

        selectManager.healthNum.text = characterData.MaxHealth.ToString();
        selectManager.damageNum.text = characterData.Damage.ToString();
        selectManager.speedNum.text = characterData.Speed.ToString();
        selectManager.critNum.text = characterData.Crit.ToString();
    }

    public void UpdatePreview()
    {
        switch (characterData.ID)
        {
            case 0:
                selectManager.animator.SetLayerWeight(selectManager.animator.GetLayerIndex("Ame Layer"), 1);
                selectManager.animator.SetLayerWeight(selectManager.animator.GetLayerIndex("Gura Layer"), 0);
                break;
            case 1:
                selectManager.animator.SetLayerWeight(selectManager.animator.GetLayerIndex("Ame Layer"), 0);
                selectManager.animator.SetLayerWeight(selectManager.animator.GetLayerIndex("Gura Layer"), 1);
                break;
            default:
                break;
        }
    }

    public void SaveDataAndSwitchScene()
    {
        PlayerPrefs.SetInt("CharacterID", id);
        PlayerPrefs.Save();
        SceneManager.LoadScene("SampleScene");
    }

}
