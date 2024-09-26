using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class StartingSequence : MonoBehaviour
{
    [SerializeField] Light2D outsideLight;
    [SerializeField] Light2D worldLight;
    [SerializeField] GameObject door;

    private void Start()
    {
        worldLight.intensity = 0;
        StartCoroutine(StartSequence());
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
        TextScroll.instance.DisplayText("introdialogue");
        yield return new WaitForSeconds(1);
        bool whileStop = true;
        while (whileStop)
        {
            if (!TextScroll.instance.gameObject.activeSelf)
            {
                whileStop = false;
                while(door.transform.position.x < 3.5f)
                {
                    door.transform.position += Vector3.right * Time.deltaTime;
                    yield return null;
                }
            }
            yield return new WaitForFixedUpdate();
        }
        GameObject.Find("Player").GetComponent<Player>().canMove = true;
        yield return null;
    }
}
