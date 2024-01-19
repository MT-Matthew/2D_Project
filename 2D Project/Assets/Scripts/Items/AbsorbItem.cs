using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorbItem : Item
{
    protected override void ApplyModifier()
    {
        player.currentAbsorb += itemData.Multipler;
    }
}
