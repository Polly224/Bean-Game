using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardboardBox : NPC
{
    public override void PromptDialogue()
    {
        if (Player.beansCollected)
        {
            SceneManager.LoadScene("House");
        }
        else
        {
            TextScroll.instance.gameObject.SetActive(true);
            TextScroll.instance.DisplayText(textId);
        }
    }
}
