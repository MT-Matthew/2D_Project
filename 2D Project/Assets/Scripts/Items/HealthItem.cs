using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : Item
{
    protected override void ApplyModifier()
    {

        float plusPoint = itemData.Multipler;

        player.maxHealth += plusPoint;

        if ((player.GetComponent<PlayerDamage>().Health + plusPoint) > player.maxHealth)
        {
            player.GetComponent<PlayerDamage>().Health = player.maxHealth;
        }
        else
        {
            player.GetComponent<PlayerDamage>().Health += plusPoint;
        }

        player.UpdateHealthBar(player.GetComponent<PlayerDamage>().Health);
    }
}
