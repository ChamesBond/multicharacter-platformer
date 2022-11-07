using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar1: MonoBehaviour
{
    public static ProgressBar1 obj;
    public Sprite s0;
    public Sprite s1;
    public Sprite s2;
    public Sprite s3;
    public Sprite s4;
    private SpriteRenderer sr;
    private float jumpForce;
    private float jumpForceLimit;
    private float jumpForceD;
    private float jumpForceLimitD;

    void Awake() 
    {
        obj = this;
    }

    void Start ()
    {
        sr = GetComponent<SpriteRenderer>();
        jumpForceLimit = Character1.obj.superJumpLimit - Character1.obj.jumpForceB;
        jumpForceLimitD = Character1.obj.doubleJumpLimit - Character1.obj.jumpForceDB;
    }

    public void appear()
    {
        gameObject.SetActive(true);
    }

    public void Update() {
        jumpForce = Character1.obj.jumpForce - Character1.obj.jumpForceB;
        jumpForceD = Character1.obj.jumpForceD - Character1.obj.jumpForceDB; 

        if(Character1.obj.isDouble) {
            if(jumpForceD >= jumpForceLimitD / 4 && jumpForceD < jumpForceLimitD / 2) sr.sprite = s1;
            if(jumpForceD >= jumpForceLimitD / 2 && jumpForceD < (3 * jumpForceLimitD) / 4) sr.sprite = s2;
            if(jumpForceD >= (3 * jumpForceLimitD) / 4 && jumpForceD < jumpForceLimitD) sr.sprite = s3;
            if(jumpForceD >= jumpForceLimitD) sr.sprite = s4;
        }
        else {
            if(jumpForce >= jumpForceLimit / 4 && jumpForce < jumpForceLimit / 2) sr.sprite = s1;
            if(jumpForce >= jumpForceLimit / 2 && jumpForce < (3 * jumpForceLimit) / 4) sr.sprite = s2;
            if(jumpForce >= (3 * jumpForceLimit) / 4 && jumpForce < jumpForceLimit) sr.sprite = s3;
            if(jumpForce >= jumpForceLimit) sr.sprite = s4;
        }
    }

    public void disappear()
    {
        sr.sprite = s0;
        gameObject.SetActive(false);
    }

    void OnDestroy() 
    {
        obj = null;
    }
}
