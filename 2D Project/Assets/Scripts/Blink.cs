using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour
{
    public Image targetImage;

    Color color1;
    Color color2;


    void Awake()
    {
        targetImage = GetComponent<Image>();
    }

    void Start()
    {
        color1 = new Color(255, 255, 255, 0f);
        color2 = new Color(255, 255, 255, 0.5f);
    }

    void Update()
    {
        Blinking();
    }

    void Blinking()
    {
        targetImage.color = Color.Lerp(color1, color2, Mathf.PingPong(Time.time * 3.5f, 1f));
    }

}
