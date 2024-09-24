using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering.RenderGraphModule;

public class TextScroll : MonoBehaviour
{
    public TextMeshPro textMeshPro;
    private string originalText;
    public string textToSet;
    public float scrollSpeed = 0.01f;
    private void OnEnable()
    {
        originalText ??= textToSet;
        StartCoroutine(ScrollCoroutine());
    }
    IEnumerator ScrollCoroutine()
    {
        textMeshPro.text = "";
        textToSet = originalText;
        while (textMeshPro.text.Length < originalText.Length)
        {
            textMeshPro.text += textToSet[0];

            if (textToSet[0] == char.Parse(".") || textToSet[0] == char.Parse("?") || textToSet[0] == char.Parse("!"))
                yield return new WaitForSeconds(scrollSpeed * 2);

            if (textToSet[0] == char.Parse(","))
                yield return new WaitForSeconds(scrollSpeed);

            textToSet = textToSet[1..];
            yield return new WaitForSeconds(scrollSpeed);
        }
    }
}
