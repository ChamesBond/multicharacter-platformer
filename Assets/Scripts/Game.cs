// Game class 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public static Game obj;

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

    // Ending the scene
    void OnDestroy() 
    {
        obj = null;
    }
}
