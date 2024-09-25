using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public bool TextboxActive = false;
    [SerializeField] public GameObject textbox;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void SetTextboxActive()
    {
        textbox.SetActive(true);
        TextboxActive = true;
    }
}
