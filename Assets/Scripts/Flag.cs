using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    public GameObject victoryBanner;
    void OnCollisionEnter2D(Collision2D collision) {
        // Victory banner is shown when player reaches the flag
        if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("ImmunePlayer")) {
            victoryBanner.SetActive(true);
        }
    }
}
