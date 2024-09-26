using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beans : MonoBehaviour
{
    public void CollectBeans()
    {
        Player.beansCollected = true;
        Player.instance.closestInteractable = null;
        Player.instance.nearbyNPCs.Clear();
        Destroy(gameObject);
    }
}
