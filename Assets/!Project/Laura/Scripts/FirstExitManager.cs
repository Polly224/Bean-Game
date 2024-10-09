using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FirstExitManager : MonoBehaviour
{
    public static bool rainHasStarted = false;
    [SerializeField] private Light2D globalLight;
    [SerializeField] private AudioClip birdsChirping;
    [SerializeField] private AudioClip rain;
    [SerializeField] private ParticleSystem rainParticles;
    void Start()
    {
        if (!rainHasStarted)
        {
            StartCoroutine(LeaveHouse());
        }
        else
        {
            globalLight.intensity = 0.2f;
            globalLight.color = Color.blue;
            rainParticles.Play();
            GetComponent<AudioSource>().volume = 0.5f;
            GetComponent<AudioSource>().clip = rain;
            GetComponent<AudioSource>().Play();
        }
    }

    private IEnumerator LeaveHouse()
    {
        yield return new WaitForFixedUpdate();
        Player.instance.canMove = false;
        yield return null;
        for(int i = 0; i < 50; i++)
        {
            yield return new WaitForFixedUpdate();
            Player.instance.canMove = false;
        }
        globalLight.intensity = 0;
        GetComponent<AudioSource>().clip = birdsChirping;
        GetComponent<AudioSource>().volume = 0;
        GetComponent<AudioSource>().Play();
        yield return null;
        while(globalLight.intensity < 1)
        {
            globalLight.intensity += 0.3f * Time.deltaTime;
            GetComponent<AudioSource>().volume += 0.3f * Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(2);
        while(GetComponent<AudioSource>().volume > 0) GetComponent<AudioSource>().volume -= 0.3f * Time.deltaTime;
        yield return new WaitForSeconds(2);
        globalLight.intensity = 0.2f;
        globalLight.color = Color.blue;
        GetComponent<AudioSource>().volume = 1;
        GetComponent<AudioSource>().clip = rain;
        GetComponent<AudioSource>().Play();
        rainParticles.Play();
        rainHasStarted = true;
        yield return new WaitForSeconds(4);
        TextScroll.instance.gameObject.SetActive(true);
        TextScroll.instance.DisplayText("stepoutside");
        while (true)
        {
            if (!TextScroll.instance.gameObject.activeSelf)
            {
                
                GetComponent<AudioSource>().volume = 0.5f;
                yield return null;
                Player.instance.canMove = true;
                break;
            }
            yield return new WaitForFixedUpdate();
        }

        yield return null;
    }
}
