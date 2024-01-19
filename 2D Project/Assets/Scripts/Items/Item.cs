using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    protected PlayerController player;
    public ItemScriptableObject itemData;

    protected virtual void ApplyModifier()
    {

    }

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        ApplyModifier();
    }

    void Update()
    {
        
    }
}
