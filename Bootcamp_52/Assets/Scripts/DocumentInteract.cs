using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DocumentInteract : Interactable
{
    public static GameObject documentGO;

    public Sprite ImageForDiary;
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
        documentGO.GetComponent<Image>().sprite = ImageForDiary;
        documentGO.SetActive(true);

    }

    void CloseDocument()
    {
        documentGO.SetActive(false);
    }

}



