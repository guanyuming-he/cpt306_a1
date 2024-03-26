using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager
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

    private readonly Timer levelTimer;
    private int level;
    private State state;
    private int score;

    /*********************************** Ctors ***********************************/
    
    public State getState() { return state; }
    public int getScore() { return score; }
    public void addScore(int s)
    {
        if (s <= 0)
        {
            throw new ArgumentException("can only add positive number to the score.");
        }

        score += s;
    }

    public StateManager()
    {
        // should be available before all.
        System.Diagnostics.Debug.Assert(Game.gameSingleton != null);

        level = 1;
        state = State.MAIN_UI;
        score = 0;

        // enable the timer on creation. loop = false.
        // timer will only update when the state is RUNNING
        levelTimer = new Timer(30.0f, onLevelTimerFired, false, false);
    }

    /*********************************** Methods ***********************************/
    public void startGame()
    {
        System.Diagnostics.Debug.Assert(state == State.MAIN_UI);

        level = 1;
        score = 0;
        state = State.RUNNING;
    }

    public void goHome()
    {
        System.Diagnostics.Debug.Assert(state != State.MAIN_UI && state != State.RUNNING);

        levelTimer.resetTimer();
        state = State.MAIN_UI;
    }

    public void pause()
    {
        System.Diagnostics.Debug.Assert(state == State.RUNNING);
        state = State.PAUSED;
    }

    public void resume()
    {
        System.Diagnostics.Debug.Assert(state == State.PAUSED);
        state = State.RUNNING;
    }

    /// <summary>
    /// Restart from the first level.
    /// </summary>
    public void restart()
    {
        System.Diagnostics.Debug.Assert(state == State.PAUSED);

        level = 1;
        score = 0;
        state = State.RUNNING;
    }

    public void continueGame()
    {
        System.Diagnostics.Debug.Assert(state == State.NEXT_LEVEL);
        // For this assignment, we only have two levels.
        // so I can only continue from level 1.
        System.Diagnostics.Debug.Assert(level == 1);

        ++level;
        levelTimer.resetTimer();
        state = State.RUNNING;
    }

    public void nextLevel()
    {
        System.Diagnostics.Debug.Assert(state == State.RUNNING);
        state = State.NEXT_LEVEL;
    }

    public void win()
    {
        System.Diagnostics.Debug.Assert(state == State.RUNNING);
        state = State.VICTORY;

        // save score to file.
        throw new NotImplementedException();
    }

    public void gameOver()
    {
        System.Diagnostics.Debug.Assert(state == State.RUNNING);
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
            Game.gameSingleton.nextLevel();
        }
        else if(level == 2)
        {
            Game.gameSingleton.win();
        }
    }

    /*********************************** MonoBehaviour ***********************************/

    public void update(float dt)
    {
        if(state != State.RUNNING)
        {
            return;
        }

        levelTimer.update(dt);
    }
}
