using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2 : MonoBehaviour
{
    Skill skillControl;
    PlayerController playerController;
    public Collider2D playerCollider;

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
        playerController.Damageable = false;

        playerController.currentSpeed += 2f;
        playerController.currentCrit += 15f;
        playerController.currentDamage += 1f;
        playerController.currentHaste += 10f;
        playerController.currentLifeSteal += 10f;
    }

    void Deactive()
    {
        playerController.Damageable = true;

        playerController.currentSpeed -= 2f;
        playerController.currentCrit -= 15f;
        playerController.currentDamage -= 1f;
        playerController.currentHaste -= 10f;
        playerController.currentLifeSteal -= 10f;
    }

    void OnDestroy()
    {
        Deactive();
        skillControl.isActive = false;
        skillControl.isReady = false;
    }
}
