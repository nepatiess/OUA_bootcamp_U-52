using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillsTube : Collectable
{
    public float healthIncreaseAmount = 10f; // Sa�l�k art�� miktar�

    public override void Collect()
    {
        base.Collect();
        Inventory.Instance.AddItem(this); // Envantere ekle
        PlayerHealth.Instance.IncreaseHealth(healthIncreaseAmount); // Oyuncu sa�l���n� art�r
        Destroy(gameObject); // Nesneyi sahneden kald�r
    }
}
