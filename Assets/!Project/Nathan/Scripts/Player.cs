using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject textbox;
    [SerializeField] float movementspeed;
    [SerializeField] Rigidbody2D rb2d;
    private Vector2 moveInput;
    public bool NPCnearby = false;
    private bool TextboxActive = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TextboxActive != true)
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
            Debug.Log("NPC nearby, Press E to interact");
            NPCnearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NPC"))
        {
            Debug.Log("NPC nearby, Press E to interact");
            NPCnearby = false;
        }
    }
    private void InteractingWithNPC()
    {
        if (Input.GetKey(KeyCode.E) && NPCnearby == true)
        {
            textbox.SetActive(true);
            TextboxActive = true;
        }
    }
}
