using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBox : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player-Hitbox"))
        {
            Transform other2 = other.transform.parent;
            if (other2 == null) return;

            Transform other3 = other2.transform.parent;
            if (other3 == null) return;

            GameObject.FindGameObjectWithTag("Box-Open").GetComponent<AudioSource>().Play();
            other3.SendMessage("LootBox");
            Destroy(gameObject);
        }
    }
}
