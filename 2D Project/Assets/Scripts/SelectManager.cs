using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectManager : MonoBehaviour
{
    public GameObject portraitLeft;
    public GameObject portraitRight;

    public RectTransform imageLayer;

    public TMP_Text characterName;

    public Image attackIcon;
    public TMP_Text attackName;
    public TMP_Text attackDescription;

    public Image skillIcon;
    public TMP_Text skillName;
    public TMP_Text skillDescription;

    public Image healthBar;
    public Image damageBar;
    public Image speedBar;
    public Image critBar;

    public TMP_Text healthNum;
    public TMP_Text damageNum;
    public TMP_Text speedNum;
    public TMP_Text critNum;

    public Animator animator;

    float count;
    bool isRun;

    void Start()
    {
        Time.timeScale = 1;
    }

    void Update()
    {
        count += Time.deltaTime;
        if (count >= 3.5f)
        {
            if (isRun)
            {
                animator.SetBool("isRun", false);
                isRun = false;
                count = 0;
            }
            else
            {
                animator.SetBool("isRun", true);
                isRun = true;
                count = 0;
            }
        }
    }
}
