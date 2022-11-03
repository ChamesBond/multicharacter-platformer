using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2 : MonoBehaviour
{
    public static Character2 obj;

    // Character states variables:
    public bool isGrounded = false;
    public bool isCharging = false;
    //public bool didImmune = false;

    // Character movement variables:
    public float speed = 5f;
    public float jumpForce = 9f;
    public float explodeForce = 3f;
    public float explodeLimit = 10f;
    public float explodeReleaseLimit = 8f;
    public float chargingTimeInS = 3f;
    public float movX;

    // Variables important to checking contact with the ground:
    public LayerMask groundLayer;
    public float radius = 0.3f;
    public float groundRayDist = 0.5f;

    // Private variables:
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private float explodeForceB;

    void Awake() {
        obj = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Getting referenced components
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        //Setting up backup values:
        explodeForceB = explodeForce;

        // Basic color
        sr.material.color = Color.blue;
    }

    // Update is called once per frame
    void Update()
    {
        // Player movement:
        movX = Input.GetAxisRaw("Horizontal");

        // Setting all character states
        isGrounded = Physics2D.CircleCast(transform.position, radius, Vector3.down, groundRayDist, groundLayer);

        if(Input.GetKeyDown(KeyCode.UpArrow)) {
            Jump();

            if(explodeForce >= explodeReleaseLimit) Explode();
        }

        if(Input.GetKeyDown(KeyCode.DownArrow)) {
            // Giving immunity
            obj.tag = "ImmunePlayer";

            // Changing color to indicate immunity
            sr.material.color = Color.grey;

            // Starting the charging
            isCharging = true;
        }

        // States in which the explosion force charges
        if(isCharging && isGrounded) {
            // Increasing force of the explosion
            explodeForce += Time.deltaTime * ((explodeLimit - explodeForceB) / chargingTimeInS);

            // Reaching the limit causes the explosion
            if(explodeForce >= explodeLimit) Explode();
        }

        // Color for available explosion
        if(explodeForce >= explodeReleaseLimit) sr.material.color = Color.cyan;
    }

    // Update for all the physics calculations connected to the Unity engine:
    void FixedUpdate() {
        rb.velocity = new Vector2(movX * speed, rb.velocity.y);
    }

    // Simple function for jumping:
    public void Jump() {
        
        if(!isGrounded || isCharging) return;

        rb.velocity = Vector2.up * jumpForce;
    }

    // Function to immunity explode
    public void Explode() {
        
        // Explosion jump
        rb.velocity = Vector2.up * explodeForce;

        // Showing the animation
        ExplosionManager.obj.showExplosion(transform.position);

        // Resetting immunity and variables
        obj.tag = "Player";
        explodeForce = explodeForceB;
        isCharging = false;
        sr.material.color = Color.blue;
    }

    void OnDestroy() {
        obj = null;
    }
}
