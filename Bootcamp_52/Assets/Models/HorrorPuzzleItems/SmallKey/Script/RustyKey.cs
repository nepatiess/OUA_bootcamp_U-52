using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RustyKey : Collectable
{
    public override void Collect()
    {
        base.Collect();
        Inventory.Instance.AddItem(this); // Envantere ekle
        Destroy(gameObject); // Nesneyi sahneden kaldýr
    }
}