using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    public static GameObject InteractLayerGO;
    char InteractKey = 'E';
    [SerializeField]
    protected string interactLayerContext = "Etkileþim için (E) tuþuna basýnýz.";
    bool isPlayerintheTrigger;
    // Start is called before the first frame update
    protected void Start()
    {
        Debug.Log("bura girdim father");

        if (InteractLayerGO == null)
        {
            InteractLayerGO = GameObject.Find("InteractLabels");
            InteractLayerGO.SetActive(false);
        }
    }

    protected void Update()
    {
        if (isPlayerintheTrigger && Input.GetKeyDown(KeyCode.E))
        {
            Action();
            CloseInteractLabel();
        }
    }





    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShowInteractLabel();
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CloseInteractLabel();
        }
    }

    void ShowInteractLabel()
    {
        isPlayerintheTrigger = true;
        InteractLayerGO.SetActive(true);

        TextMeshProUGUI textMesh = InteractLayerGO.GetComponentInChildren<TextMeshProUGUI>();
        textMesh.text = interactLayerContext;
    }

    void CloseInteractLabel()
    {
        InteractLayerGO.SetActive(false);
        isPlayerintheTrigger = false;
    }

     public virtual void Action() { }
   
}
