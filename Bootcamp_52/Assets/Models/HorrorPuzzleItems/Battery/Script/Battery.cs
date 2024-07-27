using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : Collectable
{
    //private FlashLightController flashLight;

    /*private void Start()
    {
        flashLight = GameObject.FindObhjectOfType<FlashLightController>();
    }*/

    public override void Collect()
    {
        base.Collect();
        //kullanýcýnýn batarya sayýsýný arttýrýr.
        /* if(flashLight)
        {
        flashLight.IncreaseBatteryCount();
        }*/
        Inventory.Instance.AddItem(this); // Envantere ekle
        Destroy(gameObject); // Nesneyi sahneden kaldýr
    }
}
