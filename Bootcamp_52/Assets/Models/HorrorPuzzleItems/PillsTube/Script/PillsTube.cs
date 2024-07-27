using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillsTube : Collectable
{
    public float healthIncreaseAmount = 10f; // Saðlýk artýþ miktarý

    public override void Collect()
    {
        base.Collect();
        Inventory.Instance.AddItem(this); // Envantere ekle
        PlayerHealth.Instance.IncreaseHealth(healthIncreaseAmount); // Oyuncu saðlýðýný artýr
        Destroy(gameObject); // Nesneyi sahneden kaldýr
    }
}
