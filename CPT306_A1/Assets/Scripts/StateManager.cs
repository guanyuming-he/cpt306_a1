using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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

    public static readonly String SCORE_FILE_PATH = "game_scores.txt";
    // score...datetime
    public static readonly String SCORE_FILE_LINE_FMT = "{0}...{1}";

    /*********************************** Ctors ***********************************/
    
    public State getState() { return state; }
    public int getScore() { return score; }
    public void addScore(int s)
    {
        Game.MyDebugAssert(s > 0, "cannot add non-positive score");

        score += s;
    }

    public StateManager()
    {
        // should be available before all.
        Game.MyDebugAssert(Game.gameSingleton != null);

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
        Game.MyDebugAssert(state == State.MAIN_UI);

        level = 1;
        score = 0;
        state = State.RUNNING;
    }

    public void goHome()
    {
        Game.MyDebugAssert(state != State.MAIN_UI && state != State.RUNNING);

        levelTimer.resetTimer();
        state = State.MAIN_UI;
    }

    public void pause()
    {
        Game.MyDebugAssert(state == State.RUNNING);
        state = State.PAUSED;
    }

    public void resume()
    {
        Game.MyDebugAssert(state == State.PAUSED);
        state = State.RUNNING;
    }

    /// <summary>
    /// Restart from the first level.
    /// </summary>
    public void restart()
    {
        Game.MyDebugAssert(state == State.PAUSED);

        level = 1;
        score = 0;
        state = State.RUNNING;
    }

    public void continueGame()
    {
        Game.MyDebugAssert(state == State.NEXT_LEVEL);
        // For this assignment, we only have two levels.
        // so I can only continue from level 1.
        Game.MyDebugAssert(level == 1);

        ++level;
        levelTimer.resetTimer();
        state = State.RUNNING;
    }

    public void nextLevel()
    {
        Game.MyDebugAssert(state == State.RUNNING);
        state = State.NEXT_LEVEL;
    }

    public void win()
    {
        Game.MyDebugAssert(state == State.RUNNING);
        state = State.VICTORY;

        // save score to file.
        saveScoresToFile();
    }

    public void gameOver()
    {
        Game.MyDebugAssert(state == State.RUNNING);
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

    private void saveScoresToFile()
    {
        String scoreLine = String.Format(SCORE_FILE_LINE_FMT, score, DateTime.Now);
        if (!File.Exists(SCORE_FILE_PATH))
        {
            // Create the file to write the score line to.
            using (StreamWriter sw = File.CreateText(SCORE_FILE_PATH))
            {
                sw.WriteLine(scoreLine);
            }
        }
        else
        {
            // append the score line to the existing file
            File.AppendAllLines(SCORE_FILE_PATH, new List<String> { scoreLine });
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
