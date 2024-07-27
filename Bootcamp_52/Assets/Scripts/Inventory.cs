using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance; // Singleton instance
    private HashSet<string> items; // Envanterdeki öðeler
    public event Action OnItemAdded; // Öðe eklendiðinde tetiklenecek event


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            items = new HashSet<string>();
            DontDestroyOnLoad(gameObject); // Envanterin sahne deðiþimlerinde kaybolmamasý için
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddItem(Collectable item)
    {
        string itemName = item.GetType().Name;
        items.Add(itemName); // Öðeyi envantere ekle
        Debug.Log(itemName + " added to inventory.");
        OnItemAdded?.Invoke(); // Öðe eklendiðinde event'i tetikle
    }

    public bool HasItem(string itemName)
    {
        bool hasItem = items.Contains(itemName);
        Debug.Log("Checking for item: " + itemName + " - Found: " + hasItem);
        return hasItem; // Envanterde bu öðe var mý kontrol et
    }

    public void RemoveItem(string itemName)
    {
        if (items.Contains(itemName))
        {
            items.Remove(itemName); // Öðeyi envanterden çýkar
            Debug.Log(itemName + " removed from inventory.");
            OnItemAdded?.Invoke(); // Öðe çýkarýldýðýnda event'i tetikle
        }
    }

    public IEnumerable<string> GetItems()
    {
        return items; // Envanterdeki öðeleri döndür
    }
}