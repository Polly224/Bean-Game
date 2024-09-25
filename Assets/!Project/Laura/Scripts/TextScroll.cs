using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Rendering.RenderGraphModule;

public class TextScroll : MonoBehaviour
{
    public TextMeshPro textMeshPro;
    public TextMeshPro nameText;
    private string originalText;
    public DialogueStorage.Conversation textToSet;
    public float scrollSpeed = 0.01f;
    public string textId;
    private bool canContinue = false;
    private bool canPressSpace = true;
    public static TextScroll instance;
    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        else instance = this;
    }

    private void Update()
    {
        // When space is pressed, the dialogue progresses.
        if (Input.GetButtonDown("Continue") && canPressSpace) 
        { 
            canPressSpace = false;
            canContinue = true;
        }
        if (Input.GetButtonUp("Continue")) 
        { 
            canPressSpace = true;
        }
    }

    public void DisplayText(string textId)
    {
        // Gets the corresponding piece of dialogue from the given text id, and feeds it into the textbox.
        textToSet = DialogueStorage.instance.GetDialogue(textId);
        StartCoroutine(ScrollCoroutine());
    }
    IEnumerator ScrollCoroutine()
    {
        nameText.text = textToSet.name;
        for (int i = 0; i < textToSet.conversation.Length; i++)
        {
            originalText = textToSet.conversation[i];
            textMeshPro.text = "";
            textToSet.conversation[i] = originalText;
            yield return null;
            while (textMeshPro.text.Length < originalText.Length)
            {
                textMeshPro.text += textToSet.conversation[i][0];

                if (textToSet.conversation[i][0] == char.Parse(".") || textToSet.conversation[i][0] == char.Parse("?") || textToSet.conversation[i][0] == char.Parse("!"))
                    yield return new WaitForSeconds(scrollSpeed * 2);

                if (textToSet.conversation[i][0] == char.Parse(","))
                    yield return new WaitForSeconds(scrollSpeed);

                textToSet.conversation[i] = textToSet.conversation[i][1..];
                yield return new WaitForSeconds(scrollSpeed);
                if (canContinue) textMeshPro.text += textToSet.conversation[i];
                canContinue = false;
            }
            yield return new WaitForEndOfFrame();
            yield return new WaitUntil(() => canContinue);
            canContinue = false;
        }
        gameObject.SetActive(false);
    }
}
