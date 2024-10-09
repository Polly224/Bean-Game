using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    private void FixedUpdate()
    {
        if (transform.position.z != 20) transform.position = new Vector3(transform.position.x, transform.position.y, -20);
    }
}

