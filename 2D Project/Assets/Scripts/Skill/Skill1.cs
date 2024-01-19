using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1 : MonoBehaviour
{
    Skill skillControl;
    PlayerController playerController;

    void Start()
    {
        skillControl = GetComponentInParent<Skill>();
        playerController = GetComponentInParent<PlayerController>();
        Active();
    }

    // void Update()
    // {

    // }


    void Active()
    {
        playerController.currentSpeed += 4f;
        playerController.currentHaste += 400f;
        Time.timeScale = 0.5f;
    }

    void Deactive()
    {

        playerController.currentSpeed -= 4f;
        playerController.currentHaste -= 400f;
        Time.timeScale = 1f;
    }

    void OnDestroy()
    {
        Deactive();
        skillControl.isActive = false;
        skillControl.isReady = false;
    }
}
