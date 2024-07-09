using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelfSpeaking : MonoBehaviour
{
    public static GameObject speakingGO;

    private void Start()
    {
        if (speakingGO == null)
        {
            speakingGO = GameObject.Find("SpeakingLabel");
            CloseLabel();
        }
    }

    public void SelfThoughts(string context,int time)
    {
        speakingGO.GetComponentInChildren<TextMeshProUGUI>().text = context;
        speakingGO.SetActive(true);
        Invoke("CloseLabel",time);
    }


    void ShowLabel()
    {
        speakingGO.SetActive(true);
    }
    void CloseLabel()
    {
        speakingGO.SetActive(false);
    }
}
