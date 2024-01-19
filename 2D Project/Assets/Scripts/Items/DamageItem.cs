using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageItem : Item
{
    protected override void ApplyModifier()
    {
        player.currentDamage += ((player.playerData.Damage / 100) * itemData.Multipler);
    }
}
