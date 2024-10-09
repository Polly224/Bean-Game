using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering.Universal;

public class StartingSequence : MonoBehaviour
{
    [SerializeField] Light2D outsideLight;
    [SerializeField] Light2D worldLight;
    [SerializeField] GameObject door;
    [SerializeField] GameObject corpse;
    [SerializeField] GameObject youDied;
    [SerializeField] AudioClip youDiedSound;
    [SerializeField] AudioClip doorSlideSound;
    [SerializeField] AudioClip corpseSound;

    private void Start()
    {
        worldLight.intensity = 0;
        if (PlayerPrefs.GetInt("PlayerDied") == 1) StartCoroutine(CorpseSequence());
        else if (!Player.beansCollected && !Player.beansGiven) StartCoroutine(StartSequence());
        else if (!Player.beansGiven) StartCoroutine(DeathSequence());
        else StartCoroutine(GoodEnding());
        GameObject.Find("Player").GetComponent<Player>().canMove = false;
    }

    private IEnumerator StartSequence()
    {
        yield return new WaitForSeconds(3);
        while (worldLight.intensity < 0.1f)
        {
            worldLight.intensity += 0.03f * Time.deltaTime;
            yield return null;
        };
        yield return new WaitForSeconds(2);
        TextScroll.instance.gameObject.SetActive(true);
        if (PlayerPrefs.GetInt("BeansGone") == 0) TextScroll.instance.DisplayText("introdialogue");
        else TextScroll.instance.DisplayText("introdialoguealt");
        yield return new WaitForSeconds(1);
        bool whileStop = PlayerPrefs.GetInt("BeansGone") == 0;
        while (whileStop)
        {
            if (!TextScroll.instance.gameObject.activeSelf)
            {
                whileStop = false;
                GetComponent<AudioSource>().clip = doorSlideSound;
                GetComponent<AudioSource>().Play();
                while(door.transform.position.x < 3.5f)
                {
                    door.transform.position += Vector3.right * Time.deltaTime;
                    yield return null;
                }
                GetComponent<AudioSource>().Stop();
            }
            yield return new WaitForFixedUpdate();
        }
        GameObject.Find("Player").GetComponent<Player>().canMove = true;
        yield return null;
    }
    private IEnumerator DeathSequence()
    {
        worldLight.intensity = 0;
        outsideLight.intensity = 0.5f;
        door.transform.position = new Vector3(3.5f, -4.96f, 0);
        yield return new WaitForSeconds(2);
        TextScroll.instance.gameObject.SetActive(true);
        TextScroll.instance.DisplayText("playerbeans");
        yield return new WaitForSeconds(1);
        bool whileStop = true;
        while (whileStop)
        {
            if (!TextScroll.instance.gameObject.activeSelf)
            {
                PlayerPrefs.SetInt("PlayerDied", 1);
                whileStop = false;
                yield return new WaitForSeconds(2);
                Destroy(GameObject.Find("Player"));
                GetComponent<AudioSource>().clip = corpseSound;
                GetComponent<AudioSource>().Play();
                Instantiate(corpse, Vector3.zero, Quaternion.identity);
                yield return new WaitForSeconds(3);
                Instantiate(youDied, Vector3.zero, Quaternion.identity);
                GetComponent<AudioSource>().clip = youDiedSound;
                GetComponent<AudioSource>().Play();
            }
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(10);
        while(outsideLight.intensity > 0)
        {
            outsideLight.intensity -= 0.6f * Time.deltaTime;
            yield return null;
        }
        Application.Quit();
        yield return null;
    }
    IEnumerator CorpseSequence()
    {
        Destroy(GameObject.Find("Player"));
        Instantiate(corpse, Vector3.zero, Quaternion.identity);
        yield return new WaitForSeconds(3);
        while (worldLight.intensity < 0.1f)
        {
            worldLight.intensity += 0.03f * Time.deltaTime;
            yield return null;
        };
        yield return new WaitForSeconds(2);
        TextScroll.instance.gameObject.SetActive(true);
        TextScroll.instance.DisplayText("introdialoguedead");
        bool whilestop = true;
        while (whilestop)
        {
            if (!TextScroll.instance.gameObject.activeSelf)
            {
                while (worldLight.intensity > 0)
                {
                    worldLight.intensity -= 0.03f * Time.deltaTime;
                    yield return null;
                }
                Application.Quit();
            }
            yield return new WaitForFixedUpdate();
        }
    }
    IEnumerator GoodEnding()
    {
        worldLight.intensity = 0;
        outsideLight.intensity = 0.5f;
        door.transform.position = new Vector3(3.5f, -4.96f, 0);
        yield return new WaitForSeconds(2);
        TextScroll.instance.gameObject.SetActive(true);
        TextScroll.instance.DisplayText("playernobeans");
        yield return new WaitForSeconds(1);
        bool whileStop = true;
        while (whileStop)
        {
            if (!TextScroll.instance.gameObject.activeSelf)
            {
                whileStop = false;
                while (outsideLight.intensity > 0)
                {
                    outsideLight.intensity -= 0.1f * Time.deltaTime;
                    yield return null;
                }
                Application.Quit();
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
