using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidOrb : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 targetPosition;
    public GameObject acidPoolPrefab;

    public float damage;
    public float speed;

    float t = 0f;
    bool hit = false;

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!hit)
        {
            t += Time.deltaTime / 0.7f;
            if (t >= 1.0f)
            {
                t = 1.0f;
            }

            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
        }


        if (Vector3.Distance(transform.position, targetPosition) <= 0.1f)
        {
            animator.SetBool("hit", true);
        }
    }

    void Remove()
    {
        Destroy(gameObject);
    }

    void Trigger()
    {
        hit = true;
        GameObject acidPool = Instantiate(acidPoolPrefab, transform.position, Quaternion.identity);

        acidPool.GetComponent<Stat>().damage = damage;
        acidPool.GetComponent<Stat>().speed = speed;
        Destroy(acidPool, 1.5f);
    }
}
