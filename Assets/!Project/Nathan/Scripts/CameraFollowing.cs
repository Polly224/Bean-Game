using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    [SerializeField] Transform PlayerTransform;
    void Update()
    {
        transform.position = PlayerTransform.transform.position + new Vector3(0, 1, -5);
    }
}

