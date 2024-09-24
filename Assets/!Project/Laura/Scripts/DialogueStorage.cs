using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueStorage : MonoBehaviour
{
    public static DialogueStorage instance;
    
    void Awake()
    {
        if (instance != null) Destroy(gameObject);
        else instance = this;
    }

    public string[] GetDialogue(string dialogue)
    {
        switch (dialogue)
        {
            case "test1":
                return new string[] { "Here's some really, really long dialogue. You wouldn't want to just sit around, waiting for this entire thing to finish, would you? C'mon.", "And here's sentence 2." };
            case "test2":
                return new string[] { "Woah, it's a new sentence 1!", "And a new 2nd sentence too!" };
            default:
                return new string[] { "Whoops, there's no dialogue here.", "Might want to make sure you're not inputting the wrong id."};
        }
    }
}
