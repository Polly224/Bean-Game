using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool beansCollected = false;
    public bool beansSpotted = false;
    [SerializeField] float movementspeed;
    [SerializeField] Rigidbody2D rb2d;
    [SerializeField] GameObject canvas;
    private Vector2 moveInput;
    public List<GameObject> nearbyNPCs = new();
    public static Player instance;
    Animator animator;

    private void Start()
    {
        if (instance != null) Destroy(this);
        else instance = this;
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (!TextScroll.instance.gameObject.activeSelf)
        {
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");
            moveInput.Normalize();
            animator.SetBool("walkingright", Input.GetAxisRaw("Horizontal") > 0);
            animator.SetBool("walkingleft", Input.GetAxisRaw("Horizontal") < 0);
            animator.SetBool("walkingup", Input.GetAxisRaw("Vertical") > 0);
            animator.SetBool("walkingdown", Input.GetAxisRaw("Vertical") < 0);
            if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0) animator.SetBool("idle", true);
            else animator.SetBool("idle", false);
            rb2d.velocity = moveInput * movementspeed;
        }
        else
        {
            animator.SetBool("idle", true);
        }

        InteractingWithNPC();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NPC"))
        {
            nearbyNPCs.Add(collision.gameObject);
            if (!TextScroll.instance.gameObject.activeSelf)
            {
                canvas.SetActive(true);
            }
            else
            {
                canvas.SetActive(false);
            }
        }

        if (collision.gameObject.CompareTag("Beans"))
        {
            beansSpotted = true;
            if (!TextScroll.instance.gameObject.activeSelf)
            {
                canvas.SetActive(true);
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NPC"))
        {
            nearbyNPCs.Remove(collision.gameObject);
            canvas.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Beans"))
        {
            beansSpotted = false;
            canvas.SetActive(false);
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
            if (beansCollected)
            {
                beansCollected = false;
            }
            rb2d.velocity = Vector2.zero;
        }

        if (Input.GetKeyDown(KeyCode.E) && beansSpotted)
        {
            beansCollected = true;
        }
    }
}
