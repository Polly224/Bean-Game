using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    [SerializeField] float movementspeed;
    [SerializeField] Rigidbody2D rb2d;
    private Vector2 moveInput;
    public List<GameObject> nearbyNPCs = new();
    void Update()
    {
        if (!TextScroll.instance.gameObject.activeSelf)
        {
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");

            moveInput.Normalize();

            rb2d.velocity = moveInput * movementspeed;
        }

        InteractingWithNPC();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NPC"))
        {
            nearbyNPCs.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NPC"))
        {
            nearbyNPCs.Remove(collision.gameObject);
        }
    }
    private void InteractingWithNPC()
    {
        if (Input.GetKeyDown(KeyCode.E) && nearbyNPCs.Count > 0 && !TextScroll.instance.gameObject.activeSelf)
        {
            float highestDistance = 0;
            int highestDistanceIndex = 0;
            for (int i = 0; i < nearbyNPCs.Count; i++)
            {
                if (Vector2.Distance(transform.position, nearbyNPCs[i].transform.position) > highestDistance) highestDistance = Vector2.Distance(transform.position, nearbyNPCs[i].transform.position);
                highestDistanceIndex = i;
            }
            nearbyNPCs[highestDistanceIndex].GetComponent<NPC>().PromptDialogue();
        }
    }
}
