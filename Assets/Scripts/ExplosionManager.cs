// // Manager of explosions class (calling on the appear function of explosion class)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionManager : MonoBehaviour
{
    public static ExplosionManager obj;

    // Reference for the in game unity object:
    public GameObject Explosion;

    // Initializing object
    void Awake()
    {
        obj = this;
    }

    // Explosion appears at a position
    public void showExplosion(Vector3 pos)
    {
        Explosion.gameObject.GetComponent<Explosion>().appear(pos);
    }

    // Ending the scene
    void OnDestroy() 
    {
        obj = null;
    }
}
