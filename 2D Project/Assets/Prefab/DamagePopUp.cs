using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePopUp : MonoBehaviour
{
    Vector3 offset = new Vector3(0, 1, 0);
    Vector3 startPosition;

    private float moveSpeed = 3.0f;

    private GameObject player;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.localPosition += offset;
        startPosition = transform.localPosition + offset;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);

    }

    void Remove()
    {
        Destroy(gameObject);
    }
}
