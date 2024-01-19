using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Danger : MonoBehaviour
{
    public DangerScriptableObject dangerScript;


    public Image dangerBar;

    public float dangerCount;
    public float dangerMinutes = 10f;

    public bool isDanger = false;

    void Start()
    {
        dangerCount = 0f;
    }

    void FixedUpdate()
    {
    }

    void Update()
    {
        if (!isDanger)
        {
            UpdateDangerBar();
        }
    }

    void UpdateDangerBar()
    {
        dangerCount += Time.deltaTime;
        dangerBar.fillAmount = dangerCount / (dangerMinutes * 60f);

        if (dangerCount >= (dangerMinutes * 60f) && !isDanger)
        {
            isDanger = true;
            int randomIndex = Random.Range(0, dangerScript.dangers.Count);

            GameObject newDanger = Instantiate(dangerScript.dangers[randomIndex].dangerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            newDanger.transform.parent = GameObject.FindGameObjectWithTag("Player").transform;

            Destroy(newDanger, dangerScript.dangers[randomIndex].duration);
        }
    }
}
