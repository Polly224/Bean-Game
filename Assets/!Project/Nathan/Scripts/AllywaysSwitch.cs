using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AllywaysSwitch : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private Vector3 spawnLocation;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(spawnLocation != Vector3.zero)
            Player.spawnLocation = spawnLocation;
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
