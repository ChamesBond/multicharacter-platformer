using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBarManager : MonoBehaviour
{
    public static ProgressBarManager obj;

    public GameObject ProgressBar1;
    public GameObject ProgressBar2;
    public GameObject ProgressBar3;

    void Awake()
    {
        obj = this;
    }

    public void showProgressBar1()
    {
        ProgressBar1.gameObject.GetComponent<ProgressBar1>().appear();
        ProgressBar1.gameObject.GetComponent<ProgressBar1>().Update();
    }

    public void hideProgressBar1()
    {
        ProgressBar1.gameObject.GetComponent<ProgressBar1>().disappear();
    }

    public void showProgressBar2(float chargeTime)
    {
        ProgressBar2.gameObject.GetComponent<ProgressBar2>().appear();
    }

    public void showProgressBar3(float chargeTime)
    {
        ProgressBar3.gameObject.GetComponent<ProgressBar3>().appear();
    }

    void OnDestroy() 
    {
        obj = null;
    }
}
