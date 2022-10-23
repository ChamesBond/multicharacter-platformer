using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2 : MonoBehaviour
{
    public static Character2 obj;

    // Character states variables:
    public bool isGrounded = false;
    public bool isMoving = false;
    public bool isFalling = false;
    public bool isCharging = false;
    //public bool didImmune = false;

    // Character movement variables:
    public float speed = 5f;
    public float jumpForce = 7f;
    public float explodeForce = 3f;
    public float explodeLimit = 10f;
    public float explodeReleaseLimit = 8f;
    public float chargingTime = 3f;
    public float movX;

    // Variables important to checking contact with the ground:
    public LayerMask groundLayer;
    public float radius = 0.3f;
    public float groundRayDist = 0.5f;

    // Private variables:
    private Rigidbody2D rb;
    //private Animator anim;
    //private SpriteRenderer sr;
    private float jumpForceB;
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
        jumpForceB = jumpForce;

        //anim = GetComponent<Animator>();
        //sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Player movement:
        movX = Input.GetAxisRaw("Horizontal");

        // Setting all character states
        isMoving = (movX != 0f);
        isGrounded = Physics2D.CircleCast(transform.position, radius, Vector3.down, groundRayDist, groundLayer);
        isFalling = (rb.velocity.y < 0 && !isGrounded);

        if(Input.GetKeyDown(KeyCode.UpArrow)) {
            Jump();
        }

        if(Input.GetKey(KeyCode.DownArrow)) {
            if(explodeForce <= explodeLimit && isGrounded) { explodeForce += Time.deltaTime * ((explodeLimit - explodeForceB) / chargingTime); isCharging = true;}
            else if(explodeForce > explodeLimit) Explode();
        }

        if(Input.GetKeyDown(KeyCode.DownArrow)) {
            obj.tag = "ImmunePlayer";
        }

        if(Input.GetKeyUp(KeyCode.DownArrow)) {
            if(explodeForce >= explodeReleaseLimit) Explode();
            else if(isGrounded) Shove();
            obj.tag = "Player";
            explodeForce = explodeForceB;
            isCharging = false;

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

    public void Shove()
    {
        rb.velocity = Vector2.up;
    }

    public void Explode()
    {
        //if(!isGrounded) return;

        // Explosion jump
        rb.velocity = Vector2.up * explodeForce;

        // Showing the animation
        ExplosionManager.obj.showExplosion(transform.position);

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

    void OnDestroy() {
        obj = null;
    }
}
