using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    /*********************************** Enums ***********************************/
    public enum Level
    {
        LEVEL_1,
        LEVEL_2
    }

    public enum State
    {
        PAUSED,
        RUNNING,
        VICTORY,
        GAME_OVER,
        NEXT_LEVEL
    }

    /*********************************** Fields ***********************************/

    private Level level;
    private State state;
    private int score;

    /*********************************** Unity behaviours ***********************************/

    public void Start()
    {
        level = Level.LEVEL_1;
        state = State.RUNNING;
    }

    public void Update()
    {

    }
}
