using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionManager : MonoBehaviour
{
    public static ExplosionManager obj;

    public GameObject Explosion;

    void Awake()
    {
        obj = this;
    }

    public void showExplosion(Vector3 pos)
    {
        Explosion.gameObject.GetComponent<Explosion>().appear(pos);
    }

    void OnDestroy() 
    {
        obj = null;
    }
}
