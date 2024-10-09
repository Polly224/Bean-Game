using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PulsingLight : MonoBehaviour
{
    private float startIntensity;
    private Light2D lightComponent;
    private float val;
    // Start is called before the first frame update
    void Start()
    {
        lightComponent = GetComponent<Light2D>();
        startIntensity = lightComponent.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        val += Time.deltaTime;
        lightComponent.intensity = Mathf.Sin(val) * startIntensity + 0.2f;
        if(lightComponent.intensity < 0) lightComponent.intensity = -lightComponent.intensity;
    }
}
