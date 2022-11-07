using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar2 : MonoBehaviour
{
    public static ProgressBar2 obj;
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
