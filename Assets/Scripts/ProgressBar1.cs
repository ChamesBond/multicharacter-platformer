// Progress bar for character one class (functions appearing, disappearing and showing the ability charge progress)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar1: MonoBehaviour
{
    public static ProgressBar1 obj;

    // Sprites for showing the loading process:
    public Sprite s0;
    public Sprite s1;
    public Sprite s2;
    public Sprite s3;
    public Sprite s4;

    // Private variables:
    private SpriteRenderer sr;
    private float jumpForce;
    private float jumpForceLimit;
    private float jumpForceD;
    private float jumpForceLimitD;

    // Initializing object
    void Awake() 
    {
        obj = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Getting referenced components
        sr = GetComponent<SpriteRenderer>();

        // Setting up limit values
        jumpForceLimit = Character1.obj.superJumpLimit - Character1.obj.jumpForceB;
        jumpForceLimitD = Character1.obj.doubleJumpLimit - Character1.obj.jumpForceDB;
    }

    // Update is called once per frame
    public void Update() {
        // Setting up values important for counting progress towards the limit
        jumpForce = Character1.obj.jumpForce - Character1.obj.jumpForceB;
        jumpForceD = Character1.obj.jumpForceD - Character1.obj.jumpForceDB; 

        // Setting appropriate sprites depending on the double jump ability charging progress
        if(Character1.obj.isDouble) {
            if(jumpForceD >= jumpForceLimitD / 4 && jumpForceD < jumpForceLimitD / 2) sr.sprite = s1;
            if(jumpForceD >= jumpForceLimitD / 2 && jumpForceD < (3 * jumpForceLimitD) / 4) sr.sprite = s2;
            if(jumpForceD >= (3 * jumpForceLimitD) / 4 && jumpForceD < jumpForceLimitD) sr.sprite = s3;
            if(jumpForceD >= jumpForceLimitD) sr.sprite = s4;
        }
        // Setting appropriate sprites depending on the super jump ability charging progress
        else {
            if(jumpForce >= jumpForceLimit / 4 && jumpForce < jumpForceLimit / 2) sr.sprite = s1;
            if(jumpForce >= jumpForceLimit / 2 && jumpForce < (3 * jumpForceLimit) / 4) sr.sprite = s2;
            if(jumpForce >= (3 * jumpForceLimit) / 4 && jumpForce < jumpForceLimit) sr.sprite = s3;
            if(jumpForce >= jumpForceLimit) sr.sprite = s4;
        }
    }

    // Showing the progress bar
    public void appear()
    {
        gameObject.SetActive(true);
    }

    // Hiding the progress bar
    public void disappear()
    {
        sr.sprite = s0;
        gameObject.SetActive(false);
    }

    // Ending the scene
    void OnDestroy() 
    {
        obj = null;
    }
}
