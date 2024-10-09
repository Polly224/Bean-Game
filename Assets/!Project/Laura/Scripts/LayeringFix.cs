using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayeringFix : MonoBehaviour
{
    [SerializeField] private float customOffsetY = 0f;
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, Player.instance.transform.position.y - Player.instance.GetComponent<BoxCollider2D>().offset.y <= transform.position.y - GetComponent<BoxCollider2D>().offset.y - customOffsetY ? 4 : -4);
    }
}
