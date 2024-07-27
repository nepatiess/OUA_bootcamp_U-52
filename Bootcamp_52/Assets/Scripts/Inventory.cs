using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance; // Singleton instance
    private HashSet<string> items; // Envanterdeki ��eler
    public event Action OnItemAdded; // ��e eklendi�inde tetiklenecek event


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            items = new HashSet<string>();
            DontDestroyOnLoad(gameObject); // Envanterin sahne de�i�imlerinde kaybolmamas� i�in
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddItem(Collectable item)
    {
        string itemName = item.GetType().Name;
        items.Add(itemName); // ��eyi envantere ekle
        Debug.Log(itemName + " added to inventory.");
        OnItemAdded?.Invoke(); // ��e eklendi�inde event'i tetikle
    }

    public bool HasItem(string itemName)
    {
        bool hasItem = items.Contains(itemName);
        Debug.Log("Checking for item: " + itemName + " - Found: " + hasItem);
        return hasItem; // Envanterde bu ��e var m� kontrol et
    }

    public void RemoveItem(string itemName)
    {
        if (items.Contains(itemName))
        {
            items.Remove(itemName); // ��eyi envanterden ��kar
            Debug.Log(itemName + " removed from inventory.");
            OnItemAdded?.Invoke(); // ��e ��kar�ld���nda event'i tetikle
        }
    }

    public IEnumerable<string> GetItems()
    {
        return items; // Envanterdeki ��eleri d�nd�r
    }
}