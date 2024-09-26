using System.Collections;
using System.Collections.Generic;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;

public class CookieScript : NPC
{
    public override void PromptDialogue()
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
        DialogueStorage.cookieClickAmount++;
    }
}
