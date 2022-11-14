// Game class 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public static Game obj;
    public Camera cam;

    // Initializing object
    void Awake() 
    {
        obj = this;
    }

    // Resetting the scene    
    public void gameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Moving the camera up
    public void camShiftUp()
    {
        cam.transform.Translate(new Vector3(0.0f, 20.0f, 0.0f));
    }

    // Moving the camera down
    public void camShiftDown()
    {
        cam.transform.Translate(new Vector3(0.0f, -20.0f, 0.0f));
    }

    // Moving the camera right
    public void camShiftRight()
    {
        cam.transform.Translate(new Vector3(42.0f, 0.0f, 0.0f));
    }

    // Moving the camera left
    public void camShiftLeft()
    {
        cam.transform.Translate(new Vector3(-42.0f, 0.0f, 0.0f));
    }

    // Ending the scene
    void OnDestroy() 
    {
        obj = null;
    }
}
