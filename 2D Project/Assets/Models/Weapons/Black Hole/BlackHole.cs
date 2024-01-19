using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public float damage = 1;
    public float radius = 3;
    public float timer = 0;
    public float delay = 1f;
    public float attractionForce = 1f;

    void Update()
    {
        timer += Time.deltaTime;
        // StartCoroutine(Sucking());
        Sucking();
    }

    // IEnumerator Sucking()
    // {
    //     Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
    //     if (colliders.Length > 0)
    //     {
    //         foreach (Collider2D collider in colliders)
    //         {
    //             if (collider != null)
    //             {
    //                 if (collider.CompareTag("Enemy"))
    //                 {
    //                     Transform collider2 = collider.transform.parent;
    //                     if (collider2 != null)
    //                     {

    //                         Transform collider3 = collider2.transform.parent;
    //                         if (collider3 != null)
    //                         {

    //                             IDamageable damageableObject = collider3.gameObject.GetComponent<IDamageable>();
    //                             if (damageableObject != null)
    //                             {
    //                                 Vector3 direction = transform.position - collider3.transform.position;
    //                                 collider3.transform.position += direction * attractionForce * Time.deltaTime;
    //                                 // collider3.GetComponent<Rigidbody2D>().AddForce(direction.normalized * attractionForce * Time.deltaTime);
    //                                 if (timer >= delay)
    //                                 {
    //                                     if (damageableObject != null)
    //                                     {
    //                                         damageableObject.OnHit(damage);
    //                                         timer = 0;
    //                                         yield return null;
    //                                     }
    //                                 }
    //                             }
    //                         }
    //                     }
    //                 }
    //             }
    //         }
    //     }
    // }

    void Sucking()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        if (colliders.Length > 0)
        {
            foreach (Collider2D collider in colliders)
            {
                if (collider != null)
                {
                    if (collider.CompareTag("Enemy"))
                    {
                        Transform collider2 = collider.transform.parent;
                        if (collider2 != null)
                        {

                            Transform collider3 = collider2.transform.parent;
                            if (collider3 != null)
                            {

                                IDamageable damageableObject = collider3.gameObject.GetComponent<IDamageable>();
                                if (damageableObject != null)
                                {
                                    Vector3 direction = transform.position - collider3.transform.position;
                                    collider3.transform.position += direction * attractionForce * Time.deltaTime;
                                    // collider3.GetComponent<Rigidbody2D>().AddForce(direction.normalized * attractionForce * Time.deltaTime);
                                    if (timer >= delay)
                                    {
                                        if (damageableObject != null)
                                        {
                                            // collider3.gameObject.GetComponent<DamageableCharacter>().OnHit(damage);
                                            timer = 0;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
