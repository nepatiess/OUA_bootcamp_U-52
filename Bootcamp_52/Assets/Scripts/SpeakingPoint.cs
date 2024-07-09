using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakingPoint : MonoBehaviour
{
    [SerializeField]
    string selfThoughts;

    SelfSpeaking selfThoughtsGO;
    void Start()
    {
        if (selfThoughtsGO == null)
        {
            selfThoughtsGO = GameObject.Find("GameManager").GetComponentInChildren<SelfSpeaking>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            ShowSelfthoughts();
        }
    }
    void ShowSelfthoughts()
    {
        selfThoughtsGO.SelfThoughts(selfThoughts, 3);
    }
}
