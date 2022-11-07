using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar3 : MonoBehaviour
{
    public static ProgressBar3 obj;
    void Awake() 
    {
        obj = this;
    }

    public void appear()
    {
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
