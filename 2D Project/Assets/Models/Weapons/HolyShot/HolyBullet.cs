using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyBullet : MonoBehaviour
{
    float speed;
    public Vector3 direction;
    GameObject player;
    float distance;
    bool hit = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        speed = GetComponent<Stat>().speed;

        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 180);
    }

    void Update()
    {

        if (!hit)
        {
            transform.position += direction * speed * Time.deltaTime;
        }

        distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance >= 15f)
        {
            Remove();
        }

    }

    void Remove()
    {
        Destroy(gameObject);
    }

    void Trigger()
    {
        hit = true;
    }
}
