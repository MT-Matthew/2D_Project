using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Options : MonoBehaviour
{
    public void OnMouseHover()
    {
        GetComponentInChildren<TextMeshProUGUI>().color = new Color(0, 0, 0, 1);
    }

    public void OnMouseLeave()
    {
        GetComponentInChildren<TextMeshProUGUI>().color = new Color(1, 1, 1, 1);
    }
}


