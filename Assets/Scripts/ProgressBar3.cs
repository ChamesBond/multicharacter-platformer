// Progress bar for character three class (functions appearing, disappearing and showing the ability charge progress)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar3 : MonoBehaviour
{
    public static ProgressBar3 obj;

    // Sprites for showing the loading process:
    public Sprite s0;
    public Sprite s1;
    public Sprite s2;
    public Sprite s3;
    public Sprite s4;

    // Private variables:
    private SpriteRenderer sr;
    private float speed;
    private float speedLimit;

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
        speedLimit = Character3.obj.speedLimit - Character3.obj.speedB;
    }

    // Update is called once per frame
    public void Update()
    {
        // Setting up values important for counting progress towards the limit
        speed = Character3.obj.speed - Character3.obj.speedB;

        // Setting appropriate sprites depending on charge ability charging progress
        if(speed >= speedLimit / 4 && speed < speedLimit / 2) sr.sprite = s1;
        if(speed >= speedLimit / 2 && speed < (3 * speedLimit) / 4) sr.sprite = s2;
        if(speed >= (3 * speedLimit) / 4 && speed < speedLimit) sr.sprite = s3;
        if(speed >= speedLimit) sr.sprite = s4;
        Debug.Log(speed + ", " + speedLimit);
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
