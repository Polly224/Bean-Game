using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] string textId;
    // Start is called before the first frame update
    public virtual void PromptDialogue()
    {
        TextScroll.instance.gameObject.SetActive(true);
        TextScroll.instance.DisplayText(textId);
    }
}
