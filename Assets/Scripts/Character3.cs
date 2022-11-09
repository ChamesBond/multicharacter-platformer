// Character three class (controls and ability mechanics)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character3 : MonoBehaviour
{
    public static Character3 obj;

    // Character states variables:
    public bool isGrounded = false;
    public bool isMoving = false;
    public bool progressBar = false;

    // Character movement variables:
    public float speed = 5f;
    public float speedB;
    public float speedLimit = 15f;
    public float jumpForce = 9f;
    public float jumpForceLimit = 11f;
    public float boostChargeTime = 3f;
    public float movX;

    // Variables important to checking contact with the ground:
    public LayerMask groundLayer;
    public float radius = 0.3f;
    public float groundRayDist = 0.5f;

    // Private variables:
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private float jumpForceB;

    // Initializing object
    void Awake() 
    {
        obj = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Getting referenced components
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        //Setting up backup values
        jumpForceB = jumpForce;
        speedB = speed;

        // Basic color
        sr.material.color = Color.cyan;
    }

    // Update is called once per frame
    void Update()
    {
        // Player movement
        movX = Input.GetAxisRaw("Horizontal");

        // Setting up character states
        isMoving = (rb.velocity.x != 0f);
        isGrounded = Physics2D.CircleCast(transform.position, radius, Vector3.down, groundRayDist, groundLayer);

        if(Input.GetKeyDown(KeyCode.UpArrow)) {
            Jump();
        }

        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) {
            
            if(isMoving && isGrounded && speed <= speedLimit) {
                // Turning on UI progress bar
                if(!progressBar) { ProgressBarManager.obj.showProgressBar3(); progressBar = true; }

                // Charging the boost
                speed += Time.deltaTime * ((speedLimit - speedB) / boostChargeTime);

                // Charging the jump (after half of the boost)
                if((speed - speedB) >= ((speedLimit - speedB) / 2) && jumpForce <= jumpForceLimit) { jumpForce += Time.deltaTime * ((jumpForceLimit - jumpForceB) / (boostChargeTime / 2)); }
            }
        }

        if(Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)) {
            // Turning off UI progress bar
            if(progressBar) { ProgressBarManager.obj.hideProgressBar3(); progressBar = false; };

            // Resetting jumpforce and speed on release
            speed = speedB;
            jumpForce = jumpForceB;
        }
    }

    // Update for all the physics calculations connected to the Unity engine
    void FixedUpdate() 
    {
        rb.velocity = new Vector2(movX * speed, rb.velocity.y);
    }

    // Simple function for jumping
    public void Jump() 
    {
        if(!isGrounded) return;

        rb.velocity = Vector2.up * jumpForce;
    }

    // Ending the scene
    void OnDestroy() 
    {
        obj = null;
    }
}
