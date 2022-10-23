using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character1 : MonoBehaviour
{
    public static Character1 obj;

    // Character states variables:
    public bool isGrounded = false;
    public bool isMoving = false;
    public bool isFalling = false;
    public bool isDouble = false;
    public bool didDouble = false;

    // Character movement variables:
    public float speed = 5f;
    public float jumpForce = 7f;
    public float jumpForceD = 0f;
    public float jumpForceDMultiplicator = 1f;
    public float superJumpLimit = 15f;
    public float dragLimit = 5f;
    public float movX;

    // Variables important to checking contact with the ground:
    public LayerMask groundLayer;
    public float radius = 0.3f;
    public float groundRayDist = 0.5f;

    // Private variables:
    private Rigidbody2D rb;
    private float jumpForceB;
    private float speedB;
    //private Animator anim;
    //private SpriteRenderer sr;

    void Awake() {
        obj = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //Setting up backup values:
        jumpForceB = jumpForce;
        speedB = speed;

        //anim = GetComponent<Animator>();
        //sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Player movement left/right:
        movX = Input.GetAxisRaw("Horizontal");

        // Setting all character states
        isMoving = (movX != 0f);
        isGrounded = Physics2D.CircleCast(transform.position, radius, Vector3.down, groundRayDist, groundLayer);
        isFalling = (rb.velocity.y < 0 && !isGrounded);
        isDouble = (rb.drag != 0);
        jumpForceD = rb.drag;

        if(Input.GetKeyDown(KeyCode.UpArrow)) {
            Jump();
            if(isDouble) DoubleJump();

            // Reseting speed and jumpforce to the primary values:
            jumpForce = jumpForceB;
            speed = speedB;
        }

        // Super jump preparation:
        if(Input.GetKey(KeyCode.DownArrow)) {
            if(isGrounded) {
                // Gradually increasing jumpforce (gets to the limit in 1 second):
                if(jumpForce <= superJumpLimit) jumpForce += Time.deltaTime * (superJumpLimit - jumpForceB);

                // Gradually decreasing speed (gets to the limit in 1 second):
                if(speed >= 2) speed -= Time.deltaTime * 3;
            }

            if(isFalling && !didDouble) {
                // Gradually increasing drag (falling speed):
                if(rb.drag <= dragLimit) rb.drag += Time.deltaTime * dragLimit;
            }
        }

        // Resetting drag with ground contact:
        if(isGrounded) {
            rb.drag = 0;
            didDouble = false;
        }
    }

    // Update for all the physics calculations connected to the Unity engine:
    void FixedUpdate() {
        rb.velocity = new Vector2(movX * speed, rb.velocity.y);
    }

    // Simple function for jumping:
    public void Jump() 
    {
        if(!isGrounded) return;

        rb.velocity = Vector2.up * jumpForce;
    }

    // Function for double jumping:
    public void DoubleJump()
    {
        if(didDouble) return;

        rb.velocity = Vector2.up * jumpForceD * jumpForceDMultiplicator;
        didDouble = true;
        rb.drag = 0;
    }

    void OnDestroy() {
        obj = null;
    }
}
