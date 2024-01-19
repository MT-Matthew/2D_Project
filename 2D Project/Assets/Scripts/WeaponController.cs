using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public WeaponScriptableObject weaponData;
    float playerHaste;


    public float timer = 0;

    void Start()
    {
        Attack();
    }

    void FixedUpdate()
    {
        playerHaste = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().currentHaste;

        timer += (Time.fixedDeltaTime + (Time.fixedDeltaTime * (playerHaste / 100)));
        // float delay = ((weaponData.CooldownDuration) - (1 * (playerHaste / 100)));
        // if (delay <= 0f)
        // {
        //     delay = 0.1f;
        // }
        if (timer >= weaponData.CooldownDuration)
        {
            Attack();
            if (GetComponent<AudioSource>())
            {
                GetComponent<AudioSource>().Play();
            }
        }
    }

    protected virtual void Attack()
    {
    }
}
