using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieScript : NPC
{
    public override void PromptDialogue()
    {
        TextScroll.instance.gameObject.SetActive(true);

        if (Player.instance.beansCollected)
        {
            TextScroll.instance.DisplayText(textId + "beans");
            Player.instance.beansCollected = false;
            PlayerPrefs.SetInt("BeansGone", 1);
        }
        else
        {
            TextScroll.instance.DisplayText(textId);
        }
        DialogueStorage.instance.cookieClickAmount++;
    }
}
