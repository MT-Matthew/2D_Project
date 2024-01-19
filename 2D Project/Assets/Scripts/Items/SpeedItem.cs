using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedItem : Item
{
    protected override void ApplyModifier()
    {
        player.currentSpeed += ((player.playerData.Speed / 100) * itemData.Multipler);
    }
}
