using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class House : MonoBehaviour
{
    [SerializeField] Light2D worldLight;
    [SerializeField] Light2D spotLight;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Exit());
        }
    }

    private IEnumerator Exit()
    {
        Player.instance.canMove = false;
        while(worldLight.intensity > 0)
        {
            worldLight.intensity -= 0.05f * Time.deltaTime;
            yield return null;
        }
        while (spotLight.intensity > 0)
        {
            spotLight.intensity -= 0.6f * Time.deltaTime;
            yield return null;
        }
        Player.spawnLocation = new Vector3(-4.32f, 7.1f, 0);
        SceneManager.LoadScene("MainStreet");
        yield return null;
    }
}
