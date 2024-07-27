using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignsInteract : Interactable
{
    [SerializeField]
    string selfThoughts;

    SelfSpeaking selfThoughtsGO;
    private void Start()
    {
        if (selfThoughtsGO == null)
        {
            selfThoughtsGO = GameObject.Find("GameManager").GetComponentInChildren<SelfSpeaking>();
        }
    }


    public override void Action()
    {
        ShowSelfthoughts();
    }

    void ShowSelfthoughts()
    {
        selfThoughtsGO.SelfThoughts(selfThoughts,3);
    }
}
