using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public static Explosion obj;
    void Awake() 
    {
        obj = this;
    }

    public void appear(Vector3 pos)
    {
        transform.position = pos;
        gameObject.SetActive(true);
    }

    public void disappear()
    {
        gameObject.SetActive(false);
    }

    void OnDestroy() 
    {
        obj = null;
    }
}
