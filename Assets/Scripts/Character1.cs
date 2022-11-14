// Character one class (controls and ability mechanics)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character1 : MonoBehaviour
{
    public static Character1 obj;

    // Character states variables:
    public bool isGrounded = false;
    public bool isFalling = false;
    public bool isDouble = false;
    public bool didDouble = false;
    public bool progressBar = false;
    public bool cameraShift = false;

    // Character movement and ability variables:
    public float speed = 5f;
    public float minSpeed = 2f;
    public float jumpForce = 9f;
    public float jumpForceD = 5f;
    public float jumpForceB;
    public float jumpForceDB;
    public float superJumpLimit = 16f;
    public float doubleJumpLimit = 9f;
    public float dragLimit = 5f;
    public float chargingTimeInS = 1f;
    public float movX;

    // Variables important to checking contact with the ground:
    public LayerMask groundLayer;
    public float radius = 0.3f;
    public float groundRayDist = 0.5f;

    // Private variables:
    private Rigidbody2D rb;
    private SpriteRenderer sr;    
    private float speedB;

    // Initializing object
    void Awake() {
        obj = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Getting referenced components
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        // Setting up backup values
        jumpForceB = jumpForce;
        speedB = speed;
        jumpForceDB = jumpForceD;

        // Basic color
        sr.material.color = Color.green;
    }

    // Update is called once per frame
    void Update()
    {
        // Player movement left/right
        movX = Input.GetAxisRaw("Horizontal");

        // Setting up character states
        isGrounded = Physics2D.CircleCast(transform.position, radius, Vector3.down, groundRayDist, groundLayer);
        isFalling = (rb.velocity.y < 0 && !isGrounded);
        isDouble = (rb.drag != 0);

        if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow)) {
            // Turning off UI progress bar
            if(progressBar) { ProgressBarManager.obj.hideProgressBar1(); progressBar = false; }

            Jump();
            if(isDouble) DoubleJump();

            // Reseting speed and jumpforce to the primary values
            jumpForce = jumpForceB;
            jumpForceD = jumpForceDB;
            speed = speedB;
        }

        // Super jump and double jump charging
        if(Input.GetKey(KeyCode.DownArrow)) {
            if(isGrounded) {
                // Turning on UI progress bar
                if(!progressBar) { ProgressBarManager.obj.showProgressBar1(); progressBar = true; }

                // Gradually increasing jumpforce (gets to the limit in 1 second)
                if(jumpForce <= superJumpLimit) jumpForce += Time.deltaTime * ((superJumpLimit - jumpForceB) / chargingTimeInS);

                // Gradually decreasing speed (gets to the limit in 1 second)
                if(speed >= minSpeed) speed -= Time.deltaTime * ((speedB - minSpeed) / chargingTimeInS);
            }

            if(isFalling && !didDouble) {
                // Turning on UI progress bar
                if(!progressBar) { ProgressBarManager.obj.showProgressBar1(); progressBar = true; }

                // Gradually increasing drag (decreasing falling speed)
                if(rb.drag <= dragLimit) rb.drag += Time.deltaTime * (dragLimit / chargingTimeInS);

                // Gradually increasing double jump force
                if(jumpForceD <= doubleJumpLimit) jumpForceD += Time.deltaTime * (doubleJumpLimit / chargingTimeInS);
            }
        }

        // Resetting drag and double jump parameters with ground contact
        if(isGrounded) {
            rb.drag = 0;
            didDouble = false;
            jumpForceD = jumpForceDB;
        }

        // Turning off progress bar when it's not loading anything
        if(!isGrounded && !isFalling && progressBar) { 
            ProgressBarManager.obj.hideProgressBar1(); 
            progressBar = false; 
        }

        // Modifying the camera position
        if(!cameraShift && obj.transform.position.y >= 10) {
            Game.obj.camShiftUp();
            cameraShift = true;
        } else if(cameraShift && obj.transform.position.y < 10) {
            Game.obj.camShiftDown();
            cameraShift = false;
        }
    }

    // Update for all the physics calculations connected to the Unity engine
    void FixedUpdate() {
        rb.velocity = new Vector2(movX * speed, rb.velocity.y);
    }

    // Simple function for jumping
    public void Jump() 
    {
        if(!isGrounded) return;

        rb.velocity = Vector2.up * jumpForce;
    }

    // Function for double jumping
    public void DoubleJump()
    {
        if(didDouble) return;

        rb.velocity = Vector2.up * jumpForceD;
        didDouble = true;
        rb.drag = 0;
    }

    // Ending the scene
    void OnDestroy() {
        obj = null;
    }
}
