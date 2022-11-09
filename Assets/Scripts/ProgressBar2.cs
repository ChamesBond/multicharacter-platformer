// Progress bar for character two class (functions appearing, disappearing and showing the ability charge progress)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar2 : MonoBehaviour
{
    public static ProgressBar2 obj;

    // Sprites for showing the loading process:
    public Sprite s0;
    public Sprite s1;
    public Sprite s2;
    public Sprite s3;

    // Private variables:
    private SpriteRenderer sr;
    private float explodeForce;
    private float explodeLimit;

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

        // Setting up limit value
        explodeLimit = Character2.obj.explodeLimit - Character2.obj.explodeForceB;
    }


    // Update is called once per frame
    public void Update() 
    {
        // Setting up value important for counting progress towards the limit
        explodeForce = Character2.obj.explodeForce - Character2.obj.explodeForceB;

        // Setting appropriate sprites depending on immunity explosion ability charging progress
        if(explodeForce >= explodeLimit / 4 && explodeForce < explodeLimit / 2) sr.sprite = s1;
        if(explodeForce >= explodeLimit / 2 && explodeForce < (3 * explodeLimit) / 4) sr.sprite = s2;
        if(explodeForce >= (3 * explodeLimit) / 4) sr.sprite = s3;       
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
