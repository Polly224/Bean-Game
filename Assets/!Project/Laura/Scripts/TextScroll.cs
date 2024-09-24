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
    private string originalText;
    public string[] textToSet;
    public float scrollSpeed = 0.01f;
    public string textId;
    private bool canContinue = false;
    private bool canPressSpace = true;
    private void OnEnable()
    {
        // Gets the corresponding piece of dialogue from the given text id, and feeds it into the textbox.
        textToSet = DialogueStorage.instance.GetDialogue(textId);
        StartCoroutine(ScrollCoroutine());
    }

    private void Update()
    {
        // When space is pressed, the dialogue progresses.
        if (Input.GetKeyDown(KeyCode.Space) && canPressSpace) 
        { 
            canPressSpace = false;
            canContinue = true;
        }
        if (Input.GetKeyUp(KeyCode.Space)) 
        { 
            canPressSpace = true;
        }
    }
    IEnumerator ScrollCoroutine()
    {
        for (int i = 0; i < textToSet.Length; i++)
        {
            originalText = textToSet[i];
            textMeshPro.text = "";
            textToSet[i] = originalText;
            yield return null;
            while (textMeshPro.text.Length < originalText.Length)
            {
                textMeshPro.text += textToSet[i][0];

                if (textToSet[i][0] == char.Parse(".") || textToSet[i][0] == char.Parse("?") || textToSet[i][0] == char.Parse("!"))
                    yield return new WaitForSeconds(scrollSpeed * 2);

                if (textToSet[i][0] == char.Parse(","))
                    yield return new WaitForSeconds(scrollSpeed);

                textToSet[i] = textToSet[i][1..];
                yield return new WaitForSeconds(scrollSpeed);
                if (canContinue) textMeshPro.text += textToSet[i];
                canContinue = false;
            }
            yield return new WaitForEndOfFrame();
            yield return new WaitUntil(() => canContinue);
            canContinue = false;
        }
        gameObject.SetActive(false);
    }
}
