using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritItem : Item
{
    protected override void ApplyModifier()
    {
        player.currentCrit += itemData.Multipler;
    }
}
