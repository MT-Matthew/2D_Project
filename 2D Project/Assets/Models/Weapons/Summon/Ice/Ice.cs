using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
{
    public float speed;
    public GameObject spikePrefab;
    public float damage;
    public float decayTime;

    public Vector3 targetPosition;

    Animator animator;
    bool canMove;

    void Start()
    {
        canMove = true;
        animator = GetComponent<Animator>();
        transform.rotation = Quaternion.Euler(0f, 0f, -90f);
    }

    void Update()
    {
        if (canMove)
        {
            Vector3 direction = (targetPosition - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }

        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

        if (distanceToTarget <= 0.1)
        {
            canMove = false;
            animator.SetBool("hit", true);
        }
    }

    void Summon()
    {
        Vector3 summonPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, 0f);
        GameObject newObject = Instantiate(spikePrefab, transform.position, Quaternion.identity);

        newObject.GetComponent<Stat>().damage = damage;

        Destroy(newObject, decayTime);
    }

    void Remove()
    {
        Destroy(gameObject);
    }

    void Sound()
    {
        if (GetComponent<AudioSource>().enabled)
        {
            GetComponent<AudioSource>().Play();
        }
    }
}
