// Explosion class (functions for appearing and disappearing)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public static Explosion obj;

    // Initializing object
    void Awake() 
    {
        obj = this;
    }

    // Showing the explosion at a position
    public void appear(Vector3 pos)
    {
        transform.position = pos;
        gameObject.SetActive(true);
    }

    // Hiding the explosion
    public void disappear()
    {
        gameObject.SetActive(false);
    }

    // Ending the scene
    void OnDestroy() 
    {
        obj = null;
    }
}
