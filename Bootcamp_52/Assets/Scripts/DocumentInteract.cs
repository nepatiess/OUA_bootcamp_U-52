using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DocumentInteract : Interactable
{
    public static GameObject documentGO;
    
    [SerializeField]
    TextMeshProUGUI documentText;

    void Start()
    {
        Debug.Log("bura girdim child ");
        if (documentGO == null)
        {
            documentGO = GameObject.Find("DocumentContext");
            documentGO.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        
        if (other.CompareTag("Player"))
        {
            CloseDocument();
        }
    }

    public override void Action() 
    {
        ShowDocument();
    }


    // Notların ve dosyaların açılın içerisine verilerin aktarıldığı bölüm
    void ShowDocument()
    {
        // Objedeki tüm TextMesh özelliklerinin canvas a aktarımı...
        string json = JsonUtility.ToJson(documentText);
        JsonUtility.FromJsonOverwrite(json,documentGO.GetComponentInChildren<TextMeshProUGUI>());
        documentGO.SetActive(true);

    }

    void CloseDocument()
    {
        documentGO.SetActive(false);
    }

}



