using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blockade : MonoBehaviour
{
    // Private variable:
    private BoxCollider2D bc;
    public Character3 ch3;

    // Start is called before the first frame update
    void Start() 
    {
        // Getting referenced component
        bc = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Turning off collision when speed limit is reached
        if(ch3.enabled) {
            if(ch3.speed >= ch3.speedLimit)
                bc.enabled = false;
            else bc.enabled = true;
        }
    }
}
