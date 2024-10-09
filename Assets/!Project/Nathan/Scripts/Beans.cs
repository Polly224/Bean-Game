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
        TextScroll.instance.gameObject.SetActive(true);
        TextScroll.instance.DisplayText("beansobtained");
        Destroy(gameObject);
    }
}
