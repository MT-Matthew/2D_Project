using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scythe : MonoBehaviour
{
    public Vector3 direction;

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 180);
    }

    void Update()
    {
        Vector3 playerPosition = player.transform.position;
        transform.position = playerPosition + direction.normalized * 2;
    }

    void Remove()
    {
        Destroy(gameObject);
    }
}
