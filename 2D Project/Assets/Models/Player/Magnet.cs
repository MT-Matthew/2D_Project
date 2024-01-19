using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    private GameObject player;
    private Vector3 startScale;
    private PlayerController playerController;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        startScale = transform.localScale;
    }

    void Update()
    {
        transform.localScale = startScale * (playerController.currentPickup / 100);

        transform.position = player.transform.position;
    }
}
