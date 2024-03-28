using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    // Called when the timer is fired.
    public delegate void onFire();

    private bool disabled;
    private bool fired;
    public readonly bool loop;
    public readonly float fireTime;
    private float timeElapsed;
    public readonly onFire onFireCallback;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="fireTime">how long does it take for it to fire</param>
    /// <param name="onFireCallback">what is called when the timer fires</param>
    /// <param name="disabled">if the timer is initially disabled</param>
    /// <param name="loop">if the timer automatically resets itself to enter the next circle when it fires</param>
    public Timer(float fireTime, onFire onFireCallback, bool disabled = true, bool loop = false) 
    {
        this.disabled = disabled;
        this.fired = false;
        this.fireTime = fireTime;
        this.loop = loop;
        this.timeElapsed = 0.0f;
        this.onFireCallback = onFireCallback;
    }

    /*********************************** Methods ***********************************/

    public bool hasFired() { return fired; }

    public float getTimeElapsed() { return timeElapsed; }

    /// <summary>
    /// This is not automatically called.
    /// I must explicitly call the method.
    /// </summary>
    /// <param name="dt"></param>
    public void update(float dt)
    {
        if (disabled) return;
        if (fired) return;

        timeElapsed += dt;
        if (timeElapsed > fireTime)
        {
            fired = true;
            onFireCallback();

            if (loop)
            {
                resetTimer();
            }
        }
    }

    /// <summary>
    /// Resets the state of the timer so that
    /// fired = false; and it will fire after fireTime.
    /// </summary>
    /// <param name="enable">
    /// enable the timer as well if true.
    /// </param>
    public void resetTimer(bool enable = true)
    {
        if (enable) enableTimer();
        fired = false;
        timeElapsed = 0.0f;
    }

    public void disableTimer() { disabled = true; }
    public void enableTimer() { disabled = false; }
}
