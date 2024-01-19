using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Danger1 : MonoBehaviour
{
    Danger dangerControl;

    public GameObject rockPrefab;
    public float attackDelay;
    public float rockSpeed;
    private float count;

    void Start()
    {
        dangerControl = GameObject.FindGameObjectWithTag("Manager").GetComponent<Danger>();
    }

    void Update()
    {
        count += Time.deltaTime;
        if (count >= attackDelay)
        {
            StartCoroutine(StartComboAttack());
            count = 0f;
        }
    }

    IEnumerator StartComboAttack()
    {
        Attack();
        yield return new WaitForSeconds(0.5f);
        Attack();
    }

    void Attack()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        Vector3 playerPosition = player.transform.position;
        float randomAngle = Random.Range(0f, 360f);
        Vector3 randomDirection = new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle), 0f);
        float distance = Random.Range(1, 10);
        Vector3 randomPosition = playerPosition + randomDirection * distance;

        Vector3 targetPosition = new Vector3(randomPosition.x, randomPosition.y, 0f);
        Vector3 startPosition = new Vector3(randomPosition.x - 10, randomPosition.y + 10, 0f);

        GameObject rock = Instantiate(rockPrefab, startPosition, Quaternion.identity);

        rock.GetComponent<Meteor>().targetPosition = targetPosition;
        rock.GetComponent<Meteor>().speed = rockSpeed;

        // Destroy(rock, attackDelay);
    }

    void OnDestroy()
    {
        dangerControl.isDanger = false;
        dangerControl.dangerCount = 0f;
    }
}
