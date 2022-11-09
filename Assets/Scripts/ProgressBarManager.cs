// Manager of progress bars class (calling on all of the progress bars functions)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBarManager : MonoBehaviour
{
    public static ProgressBarManager obj;

    // All progress bars objects:
    public GameObject ProgressBar1;
    public GameObject ProgressBar2;
    public GameObject ProgressBar3;

    // Initializing object
    void Awake()
    {
        obj = this;
    }

    // Showing and activating progress bar for character one
    public void showProgressBar1()
    {
        ProgressBar1.gameObject.GetComponent<ProgressBar1>().appear();
        ProgressBar1.gameObject.GetComponent<ProgressBar1>().Update();
    }

    // Hiding the progress bar for character one
    public void hideProgressBar1()
    {
        ProgressBar1.gameObject.GetComponent<ProgressBar1>().disappear();
    }

    // Showing and activating progress bar for character two
    public void showProgressBar2()
    {
        ProgressBar2.gameObject.GetComponent<ProgressBar2>().appear();
        ProgressBar2.gameObject.GetComponent<ProgressBar2>().Update();
    }

    // Hiding the progress bar for character two
    public void hideProgressBar2()
    {
        ProgressBar2.gameObject.GetComponent<ProgressBar2>().disappear();
    }

    // Showing and activating progress bar for character three
    public void showProgressBar3()
    {
        ProgressBar3.gameObject.GetComponent<ProgressBar3>().appear();
        ProgressBar3.gameObject.GetComponent<ProgressBar3>().Update();
    }

    // Hiding the progress bar for character three
    public void hideProgressBar3()
    {
        ProgressBar3.gameObject.GetComponent<ProgressBar3>().disappear();
    }

    // Ending the scene
    void OnDestroy() 
    {
        obj = null;
    }
}
