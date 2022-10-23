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
    public float jumpForce = 3f;
    public float movX;

    // Variables important to checking contact with the ground:
    public LayerMask groundLayer;
    public float radius = 0.3f;
    public float groundRayDist = 0.5f;

    // Private variables:
    private Rigidbody2D rb;
    //private Animator anim;
    //private SpriteRenderer sr;

    void Awake() {
        obj = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //anim = GetComponent<Animator>();
        //sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Player movement:
        movX = Input.GetAxisRaw("Horizontal");

        // Checking if the character is moving:
        isMoving = (movX != 0f);

        // Checking if the character is grounded:
        isGrounded = Physics2D.CircleCast(transform.position, radius, Vector3.down, groundRayDist, groundLayer);

        if(Input.GetKeyDown(KeyCode.UpArrow)) {
            Jump();
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
