using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public Vector3 targetPosition;
    public float speed;

    void Start()
    {
        // transform.rotation = Quaternion.Euler(0f, 0f, -90f);
    }

    void Update()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

        if (distanceToTarget <= 0.01)
        {
            Destroy(gameObject);
        }
    }
}
