using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character3 : MonoBehaviour
{
    public static Character3 obj;

    // Character states variables:
    public bool isGrounded = false;
    public bool isMoving = false;

    // Character movement variables:
    public float speed = 5f;
    public float jumpForce = 9f;
    public float speedLimit = 15f;
    public float jumpForceLimit = 11f;
    public float boostChargeTime = 3f;
    public float movX;

    // Variables important to checking contact with the ground:
    public LayerMask groundLayer;
    public float radius = 0.3f;
    public float groundRayDist = 0.5f;

    // Private variables:
    private Rigidbody2D rb;
    //private Animator anim;
    //private SpriteRenderer sr;
    private float speedB;
    private float jumpForceB;

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
        // Player movement:
        movX = Input.GetAxisRaw("Horizontal");

        // Setting all character states
        isMoving = (rb.velocity.x != 0f);
        isGrounded = Physics2D.CircleCast(transform.position, radius, Vector3.down, groundRayDist, groundLayer);

        if(Input.GetKeyDown(KeyCode.UpArrow)) {
            Jump();

            // Reseting jumpforce to the primary value:
            jumpForce = jumpForceB;
        }

        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) {
            
            if(isMoving && isGrounded && speed <= speedLimit) {
                // Charging the boost
                speed += Time.deltaTime * ((speedLimit - speedB) / boostChargeTime); 
                // Charging the jump (after half of the boost)
                if((speed - speedB) >= ((speedLimit - speedB) / 2) && jumpForce <= jumpForceLimit) { jumpForce += Time.deltaTime * ((jumpForceLimit - jumpForceB) / (boostChargeTime / 2)); }
            }
        }

        if(Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)) {
            // Resetting jumpforce and speed on release
            speed = speedB;
            jumpForce = jumpForceB;
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

    void OnDestroy() {
        obj = null;
    }
}
