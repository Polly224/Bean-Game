using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatScript : NPC
{
    public static bool talkedToBefore = false;
   public override void  PromptDialogue()
    {
        if (talkedToBefore) textId = "ratcoatalt";
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
        talkedToBefore = true;
    }
}
