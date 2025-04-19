using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;


public class PlayerController : MonoBehaviour
{

    public float waitingTime = 0.099f; // Delay animation time from diagonal: Adjust this value as needed
    public bool setDiagonalMovement; //Immediately makes movement diagonal to test animaitons

    Animator animator;
    private Rigidbody2D rb;
    private Vector2 movement;
    private float lastDirection;
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    private bool isUpdatingLastDirection = false; // Prevent multiple coroutines

    private Vector2 newMovement; //just to test changes

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = movement * walkSpeed;
                
    }

    private void Update()
    {
        //if (movement != newMovement)
        //{
        //    Debug.Log("velocity: " + rb.linearVelocity);
        //    Debug.Log("velocity magnitude: " + rb.linearVelocity.magnitude);
        //    Debug.Log("lastdirection: " + lastDirection);
        //    newMovement = movement;
        //}
        animator.SetFloat("Blend", lastDirection);
        animator.SetFloat("X", rb.linearVelocity.x);
        animator.SetFloat("Y", rb.linearVelocity.y);
        animator.SetFloat("Speed", rb.linearVelocity.magnitude);
        

        
            
            if (!isUpdatingLastDirection) // If moving away from diagonal, delay before switching
            {
                StartCoroutine(DelayDirectionChange());
            }
       
    }

    private IEnumerator DelayDirectionChange()
    {
        isUpdatingLastDirection = true;
        yield return new WaitForSecondsRealtime(waitingTime); // Wait before changing direction

        float tolerance = 0.2f;

        // Determine new direction after delay

            //Down
         if (Mathf.Abs(rb.linearVelocity.x - 0) < tolerance && Mathf.Abs(rb.linearVelocity.y - -1 * walkSpeed) < tolerance)
        {
            lastDirection = 0f;
        }

        //Right-Down
        else if (Mathf.Abs(rb.linearVelocity.x - .707f * walkSpeed) < tolerance && Mathf.Abs(rb.linearVelocity.y - (-.707f * walkSpeed)) < tolerance)
        {
            lastDirection = 0.5f;
        }

        //Right
        else if (Mathf.Abs(rb.linearVelocity.x - 1 * walkSpeed) < tolerance && Mathf.Abs(rb.linearVelocity.y - 0) < tolerance)
        {
            lastDirection = 1;
        }

        //Right-Up
        else if (Mathf.Abs(rb.linearVelocity.x - (0.707f * walkSpeed)) < tolerance && Mathf.Abs(rb.linearVelocity.y - (0.707f * walkSpeed)) < tolerance)
        {
            lastDirection = 1.5f;
            Debug.Log(lastDirection);

        }

        //Up
        else if (Mathf.Abs(rb.linearVelocity.x - 0) < tolerance && Mathf.Abs(rb.linearVelocity.y - 1 * walkSpeed) < tolerance)
        {
            lastDirection = 2;
        }

        //Left-Up
        else if (Mathf.Abs(rb.linearVelocity.x - (- 0.707f * walkSpeed)) < tolerance && Mathf.Abs(rb.linearVelocity.y - (0.707f * walkSpeed)) < tolerance)
        {
            lastDirection = 2.5f;
        }

        //Left
        else if (Mathf.Abs(rb.linearVelocity.x - -1 * walkSpeed) < tolerance && Mathf.Abs(rb.linearVelocity.y - 0) < tolerance)
        {
            lastDirection = 3f;
        }

        //Left-Down
        else if (Mathf.Abs(rb.linearVelocity.x - (-0.707f * walkSpeed)) < tolerance && Mathf.Abs(rb.linearVelocity.y - (-0.707f * walkSpeed)) < tolerance)
        {
            lastDirection = 3.5f;
        }


        Debug.Log("lastDirection changed to: " + lastDirection);
        isUpdatingLastDirection = false;
    }

    public void OnMove(InputValue value)
    {
        //Debug.Log("InputValue: " + value);
        //Debug.Log("X: " + animator.GetFloat("X"));
        //Debug.Log("Y: " + animator.GetFloat("Y"));
        //Debug.Log("Blend: " + animator.GetFloat("Blend"));
        //Debug.Log("Speed: " + animator.GetFloat("Speed"));

        movement = value.Get<Vector2>();
        //Debug.Log("movement: " + movement);
        //Debug.Log("X: " + animator.GetFloat("X"));
        //Debug.Log("Y: " + animator.GetFloat("Y"));
        //Debug.Log("Blend: " + animator.GetFloat("Blend"));
        //Debug.Log("Speed: " + animator.GetFloat("Speed"));
    }


}
