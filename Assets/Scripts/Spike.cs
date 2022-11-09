// Spike class (function for checking collision)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision) {
        // Game over when player collides with the spike
        if(collision.gameObject.CompareTag("Player")) {
            Game.obj.gameOver();
        }
    }
}