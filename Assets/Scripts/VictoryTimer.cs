using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryTimer : MonoBehaviour
{
    public float afterVictoryTime = 3.0f;
    private float timePassed;
    // Update is called once per frame
    void Update()
    {
        // Counting time passed to a variable
        timePassed += Time.deltaTime;

        // Ending the game after a certain amount of time
        if(timePassed >= afterVictoryTime) {
        Game.obj.gameOver();
        }
    }
}
