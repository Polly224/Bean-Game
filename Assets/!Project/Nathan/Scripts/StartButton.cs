using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void PressedStart()
    {
        SceneManager.LoadScene(1);
    }

    public void PressedCredits()
    {
        SceneManager.LoadScene(2);
    }
}
