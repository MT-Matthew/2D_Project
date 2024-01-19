using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendItem : Item
{
    protected override void ApplyModifier()
    {
        player.currentDefend += ((player.playerData.Defend / 100) * itemData.Multipler);
    }
}
