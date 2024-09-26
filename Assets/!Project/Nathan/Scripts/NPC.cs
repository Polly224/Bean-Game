using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] public string textId;
    // Start is called before the first frame update
    public virtual void PromptDialogue()
    {
        TextScroll.instance.gameObject.SetActive(true);
        

        if (Player.beansCollected)
        {
            TextScroll.instance.DisplayText(textId + "beans");
            Player.beansCollected = false;
            Player.beansGiven = true;
            PlayerPrefs.SetInt("BeansGone", 1);
        }
        else
        {
            TextScroll.instance.DisplayText(textId);
        }
    }
}
