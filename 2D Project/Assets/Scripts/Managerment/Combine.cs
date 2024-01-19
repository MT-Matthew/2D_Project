using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combine : MonoBehaviour
{
    [Header("Experience Settings")]
    public GameObject newExpPrefab;
    Vector3 spawnPosition;

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        // CheckForCombine();
        StartCoroutine(CombineExpCoroutine());
    }

    // void CheckForCombine()
    // {
    //     Exp[] allExpObjects = FindObjectsOfType<Exp>();

    //     for (int i = 0; i < allExpObjects.Length; i++)
    //     {
    //         for (int j = i + 1; j < allExpObjects.Length; j++)
    //         {
    //             float distance = Vector3.Distance(allExpObjects[i].transform.position, allExpObjects[j].transform.position);

    //             if (distance <= 0.8 && !allExpObjects[i].canMove && !allExpObjects[j].canMove && allExpObjects[i].expValue < 200 && allExpObjects[j].expValue < 200)
    //             {
    //                 CombineExp(allExpObjects[i], allExpObjects[j]);
    //             }
    //         }
    //     }
    // }

    IEnumerator CombineExpCoroutine()
    {
        Exp[] allExpObjects = FindObjectsOfType<Exp>();

        for (int i = 0; i < allExpObjects.Length; i++)
        {
            for (int j = i + 1; j < allExpObjects.Length; j++)
            {
                if (allExpObjects[i] != null && allExpObjects[j] != null)
                {
                    float distance = Vector3.Distance(allExpObjects[i].transform.position, allExpObjects[j].transform.position);

                    if (distance <= 0.8f && !allExpObjects[i].canMove && !allExpObjects[j].canMove && allExpObjects[i].expValue < 200 && allExpObjects[j].expValue < 200)
                    {

                        CombineExp(allExpObjects[i], allExpObjects[j]);
                        yield return null; // Wait for the end of the frame

                    }
                }
            }
        }
    }

    void CombineExp(Exp obj1, Exp obj2)
    {
        int totalExpValue = obj1.expValue + obj2.expValue;

        spawnPosition = obj1.transform.position;
        GameObject newExp = Instantiate(newExpPrefab, spawnPosition, Quaternion.identity);

        Exp newExpScript = newExp.GetComponent<Exp>();
        newExpScript.expValue = totalExpValue;
        newExpScript.expPosition = spawnPosition;

        Destroy(obj1.gameObject);
        Destroy(obj2.gameObject);
    }
}
