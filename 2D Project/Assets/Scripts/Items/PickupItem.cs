using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : Item
{
    protected override void ApplyModifier()
    {
        player.currentPickup += ((player.playerData.Pickup / 100) * itemData.Multipler);
    }
}
