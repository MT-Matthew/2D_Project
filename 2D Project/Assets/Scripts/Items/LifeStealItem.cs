using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeStealItem : Item
{
    protected override void ApplyModifier()
    {
        player.currentLifeSteal += itemData.Multipler;
    }
}
