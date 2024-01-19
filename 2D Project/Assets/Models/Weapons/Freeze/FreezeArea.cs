using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeArea : MonoBehaviour
{
    public float freezeTime;
    public float radius;

    void OnTriggerEnter2D(Collider2D other)
    {
        Transform other2 = other.transform.parent;
        if (other2 == null) return;

        Transform other3 = other2.transform.parent;
        if (other3 == null) return;

        if (other3.CompareTag("Enemy"))
        {
            other3.GetComponent<EnemyStat>().isFreeze = true;
            other3.GetComponent<EnemyStat>().freezeDuration = freezeTime;
            other3.GetComponent<EnemyStat>().countFreezeTime = 0;
        }
    }
}
