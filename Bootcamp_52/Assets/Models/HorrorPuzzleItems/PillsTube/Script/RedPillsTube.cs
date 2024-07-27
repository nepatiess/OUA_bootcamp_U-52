using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPillsTube : Collectable
{
    public float healthDecreaseAmount = 10f; // Saðlýk artýþ miktarý

    public override void Collect()
    {
        base.Collect();
        Inventory.Instance.AddItem(this); // Envantere ekle
        PlayerHealth.Instance.IncreaseHealth(healthDecreaseAmount); // Oyuncu saðlýðýný artýr
        Destroy(gameObject); // Nesneyi sahneden kaldýr
    }
}
