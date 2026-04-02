using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class PlayerController : MonoBehaviour
{
 // Rigidbody of the player.
 private Rigidbody rb; 

 private int count;

 // Movement along X and Y axes.
 private float movementX;
 private float movementY;
 public float jumpForce = 5f;
 private bool isGrounded;
 private int isDouble = 1;


 // Speed at which the player moves.
 public float speed = 0; 
 public TextMeshProUGUI countText;

 public GameObject winTextObject;


 // Start is called before the first frame update.
 void Start()
    {
 // Get and store the Rigidbody component attached to the player.
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);

    }
 
 // This function is called when a move input is detected.
 void OnMove(InputValue movementValue)
    {
 // Convert the input value into a Vector2 for movement.
        Vector2 movementVector = movementValue.Get<Vector2>();

 // Store the X and Y components of the movement.
        movementX = movementVector.x; 
        movementY = movementVector.y; 
    }

void OnJump(InputValue value)
    {
        if (isGrounded)
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        
        else if (isDouble == 1)
        {
            isDouble -= 1;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

void OnCollisionStay(Collision collision)
{
    foreach (ContactPoint contact in collision.contacts)
    {
        if (contact.normal.y > 0.5f)
        {
            isGrounded = true;
            break;
        }
    }
}

void OnCollisionExit(Collision collision)
{
    isGrounded = false;
    isDouble = 1;
}

void SetCountText() 
   {
       countText.text =  "Count: " + count.ToString();
       if (count >= 20)
       {
           winTextObject.SetActive(true);
       }
   }

 // FixedUpdate is called once per fixed frame-rate frame.
    void FixedUpdate() 
    {
 // Create a 3D movement vector using the X and Y inputs.
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);

 // Apply force to the Rigidbody to move the player.
        rb.AddForce(movement * speed); 
    }

    void OnTriggerEnter(Collider other) 
   {
       if (other.gameObject.CompareTag("PickUp")) 
       {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
       } 
   }
}