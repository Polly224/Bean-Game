using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearAfterBeanNPC : NPC
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
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            TextScroll.instance.DisplayText(textId);
        }
    }
}
