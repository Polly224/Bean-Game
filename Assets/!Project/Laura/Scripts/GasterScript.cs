using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasterScript : NPC
{
    public static bool beenTalkedTo = false;

    private void Start()
    {
        if(beenTalkedTo) Destroy(gameObject);
    }
    public override void PromptDialogue()
    {
        TextScroll.instance.gameObject.SetActive(true);
        beenTalkedTo = true;

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
        StartCoroutine(WaitTilDialogueGone());
    }

    private IEnumerator WaitTilDialogueGone()
    {
        yield return new WaitForFixedUpdate();
        bool doneYet = true;
        while (doneYet)
        {
            yield return new WaitForFixedUpdate();
            if (!TextScroll.instance.gameObject.activeSelf)
            {
                GetComponent<AudioSource>().Play();
                GetComponent<SpriteRenderer>().size = new Vector2(0, 0);
                GetComponent<BoxCollider2D>().transform.position += Vector3.one * 1000;
                Player.instance.nearbyNPCs.Clear();
                doneYet = false;
                yield return null;
            }
        }
        yield return null;
    }
}
