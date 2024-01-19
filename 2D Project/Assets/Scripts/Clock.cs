using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    Timer timer;
    public RectTransform hand;

    public float hoursToDegrees = 360 / 24;

    void Start()
    {
        timer = GameObject.FindGameObjectWithTag("Manager").GetComponent<Timer>();
    }

    void Update()
    {
        hand.rotation = Quaternion.Euler(0, 0, (-timer.hours + 6) * hoursToDegrees);
    }
}
