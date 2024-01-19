using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasteItem : Item
{
    protected override void ApplyModifier()
    {
        player.currentHaste += itemData.Multipler;
    }
}
