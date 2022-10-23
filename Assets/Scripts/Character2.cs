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
    //private Animator anim;
    //private SpriteRenderer sr;
    private float explodeForceB;

    void Awake() {
        obj = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //sr = GetComponent<SpriteRenderer>();

        //Setting up backup values:
        explodeForceB = explodeForce;

        //anim = GetComponent<Animator>();
        //sr = GetComponent<SpriteRenderer>();
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
        }

        if(Input.GetKey(KeyCode.DownArrow)) {
            // Charging the explosion
            if(explodeForce <= explodeLimit && isGrounded) { explodeForce += Time.deltaTime * ((explodeLimit - explodeForceB) / chargingTimeInS); isCharging = true; }
            // Explosion when reaching a limit
            else if(explodeForce > explodeLimit) Explode();
        }

        if(Input.GetKeyDown(KeyCode.DownArrow)) {
            // Giving immunity
            obj.tag = "ImmunePlayer";
        }

        if(Input.GetKeyUp(KeyCode.DownArrow)) {
            // Explosion when releasing the down arrow (only when reached certain release limit)
            if(explodeForce >= explodeReleaseLimit) Explode();
            // Shove to register the collider (so you cannot stay indefinitely on the spikes)
            else Shove();
        }

        /*
        // Immunity for a certain duration
        if(Input.GetKey(KeyCode.DownArrow)) {
            if(!didImmune) {
            obj.tag = "ImmunePlayer";

            // Charging the explosion
            if(explodeForce <= explodeLimit && isGrounded) { explodeForce += Time.deltaTime * ((explodeLimit - explodeForceB) / 3); isCharging = true;}
            // Explosion when reaching a limit
            else Explode();
            }
        }

        // Resetting immunity
        if(Input.GetKeyDown(KeyCode.DownArrow)) {
            didImmune = false;
        }

        // Explosion when releasing
        if(Input.GetKeyUp(KeyCode.DownArrow)) {
            // Resetting immunity
            if(didImmune) didImmune = false;
            // Explosion when releasing the down arrow
            else Explode();

            // Backup resetting (important when releasing in the air)
            obj.tag = "Player";
            explodeForce = explodeForceB;
            isCharging = false;
        }
        */
    }

    // Update for all the physics calculations connected to the Unity engine:
    void FixedUpdate() {
        rb.velocity = new Vector2(movX * speed, rb.velocity.y);
    }

    // Simple function for jumping:
    public void Jump() 
    {
        if(!isGrounded || isCharging) return;

        rb.velocity = Vector2.up * jumpForce;
    }

    // Function to immunity explode
    public void Explode()
    {
        //if(!isGrounded) return;

        // Explosion jump
        rb.velocity = Vector2.up * explodeForce;

        // Showing the animation
        ExplosionManager.obj.showExplosion(transform.position);


        // Resetting immunity and variables
        obj.tag = "Player";
        explodeForce = explodeForceB;
        isCharging = false;


        /*
        // Resetting immunity and variables
        obj.tag = "Player";
        explodeForce = explodeForceB;
        didImmune = true;
        isCharging = false;
        */
    }

    // Function important in registering the collider, moves up a few pixels
    public void Shove()
    {
        if(!isGrounded) return;

        // Delicate shove
        rb.velocity = Vector2.up;

        // Resetting immunity and variables
        obj.tag = "Player";
        explodeForce = explodeForceB;
        isCharging = false;
    }

    void OnDestroy() {
        obj = null;
    }
}
