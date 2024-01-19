using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Danger2 : MonoBehaviour
{
    Danger dangerControl;

    ParticleSystem rain;
    GameObject lightBeam;

    void Start()
    {
        dangerControl = GameObject.FindGameObjectWithTag("Manager").GetComponent<Danger>();
        lightBeam = GameObject.FindGameObjectWithTag("Light Beam");
        rain = transform.Find("Rain").GetComponent<ParticleSystem>();

        transform.Find("Rain").transform.position = new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x, GameObject.FindGameObjectWithTag("Player").transform.position.y + 9f, 0f);

        rain.Play();
        lightBeam.SetActive(false);
    }

    // void Update()
    // {

    // }

    void OnDestroy()
    {
        lightBeam.SetActive(true);
        rain.Stop();
        dangerControl.isDanger = false;
        dangerControl.dangerCount = 0f;
    }
}
