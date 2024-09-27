using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static bool beansCollected = false;
    public static bool beansGiven = false;
    [SerializeField] float movementspeed;
    [SerializeField] Rigidbody2D rb2d;
    [SerializeField] GameObject canvas;
    public GameObject closestInteractable;
    private Vector2 moveInput;
    public List<GameObject> nearbyNPCs = new();
    public static Player instance;
    Animator animator;
    public bool canMove = true;

    private void Start()
    {
        if (instance != null) Destroy(this);
        else instance = this;
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) PlayerPrefs.SetInt("BeansGone", 0);
        if (!TextScroll.instance.gameObject.activeSelf && canMove) 
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

        float highestDistance = 0;
        int highestDistanceIndex = 0;
        for (int i = 0; i < nearbyNPCs.Count; i++)
        {
            if (Vector2.Distance(transform.position, nearbyNPCs[i].transform.position) > highestDistance) highestDistance = Vector2.Distance(transform.position, nearbyNPCs[i].transform.position);
            highestDistanceIndex = i;
        }
        if (nearbyNPCs.Count != 0) closestInteractable = nearbyNPCs[highestDistanceIndex];
        else closestInteractable = null;
        if (!TextScroll.instance.gameObject.activeSelf && !canvas.activeSelf && closestInteractable != null && canMove) canvas.SetActive(true);
        else if (TextScroll.instance.gameObject.activeSelf && canvas.activeSelf) canvas.SetActive(false);
        if(closestInteractable != null) canvas.transform.position = closestInteractable.transform.position + Vector3.up * 2;
        InteractingWithNPC();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NPC"))
        {
            nearbyNPCs.Add(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Beans"))
        {
            nearbyNPCs.Add(collision.gameObject);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NPC"))
        {
            nearbyNPCs.Remove(collision.gameObject);
            if(nearbyNPCs.Count == 0) canvas.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Beans"))
        {
            canvas.SetActive(false);
        }
    }
    private void InteractingWithNPC()
    {
        if (Input.GetKeyDown(KeyCode.E) && nearbyNPCs.Count > 0 && !TextScroll.instance.gameObject.activeSelf && canMove)
        {
            if (closestInteractable.CompareTag("NPC")) closestInteractable.GetComponent<NPC>().PromptDialogue();
            else if (closestInteractable.CompareTag("Beans")) closestInteractable.GetComponent<Beans>().CollectBeans();
            rb2d.velocity = Vector2.zero;
        } 
    }
}
