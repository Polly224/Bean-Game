using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatScript : NPC
{
   public override void  PromptDialogue()
    {
        TextScroll.instance.gameObject.SetActive(true);


        if (Player.instance.beansCollected)
        {
            TextScroll.instance.DisplayText(textId + "beans");
            Player.instance.beansCollected = false;
        }
        else
        {
            TextScroll.instance.DisplayText(textId);
        }
        textId = "ratcoatalt";
    }
}
