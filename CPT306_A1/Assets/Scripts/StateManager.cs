using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    /*********************************** States ***********************************/
    public enum State
    {
        MAIN_UI,
        PAUSED,
        RUNNING,
        VICTORY,
        GAME_OVER,
        NEXT_LEVEL
    }

    /*********************************** Fields ***********************************/

    private Timer levelTimer;
    private int level;
    private State state;
    private int score;

    /*********************************** Ctors ***********************************/
    public StateManager()
    {
        level = 1;
        state = State.MAIN_UI;
        score = 0;

        // enable the timer on creation. loop = false.
        levelTimer = new Timer(30.0f, onLevelTimerFired, false, false);
    }

    /*********************************** Methods ***********************************/
    public void startGame()
    {
        Debug.Assert(state == State.MAIN_UI);
        state = State.RUNNING;
    }

    public void goHome()
    {
        Debug.Assert(state != State.MAIN_UI && state != State.RUNNING);
        state = State.MAIN_UI;
    }

    public void pause()
    {
        Debug.Assert(state == State.RUNNING);
        state = State.PAUSED;
    }

    public void resume()
    {
        Debug.Assert(state == State.PAUSED);
        state = State.RUNNING;
    }

    public void continueGame()
    {
        Debug.Assert(state == State.NEXT_LEVEL);
        Debug.Assert(level == 1);

        state = State.RUNNING;
        ++level;
        levelTimer.resetTimer();
    }

    private void nextLevel()
    {
        Debug.Assert(state == State.RUNNING);
        state = State.NEXT_LEVEL;
    }

    private void win()
    {
        Debug.Assert(state == State.RUNNING);
        state = State.VICTORY;

        // save score to file.
        throw new Exception("Unfinished.");
    }

    public void gameOver()
    {
        Debug.Assert(state == State.RUNNING);
        state = State.GAME_OVER;
    }

    /// <summary>
    /// `level = 1 -> nextLevel()`
    /// `level = 2 -> win()`
    /// </summary>
    public void onLevelTimerFired()
    {
        if(level == 1)
        {
            nextLevel();
        }
        else if(level == 2)
        {
            win();
        }
    }

    /*********************************** MonoBehaviour ***********************************/

    public void Update()
    {
        if(state != State.RUNNING)
        {
            return;
        }

        levelTimer.update(Time.deltaTime);
    }
}
