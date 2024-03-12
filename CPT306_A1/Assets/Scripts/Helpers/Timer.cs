using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    // Called when the timer is fired.
    public delegate void onFire();

    private bool paused;
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
    /// <param name="paused">if the timer is initially paused</param>
    /// <param name="loop">if the timer automatically resets itself to enter the next circle when it fires</param>
    public Timer(float fireTime, onFire onFireCallback, bool paused = true, bool loop = false) 
    {
        this.paused = paused;
        this.fired = false;
        this.fireTime = fireTime;
        this.loop = loop;
        this.timeElapsed = 0.0f;
        this.onFireCallback = onFireCallback;
    }

    /*********************************** Unity behaviours ***********************************/

    // Update is called once per frame
    void Update()
    {
        if (paused) return;
        if (fired) return; 

        timeElapsed += Time.deltaTime;
        if(timeElapsed > fireTime) 
        {
            fired = true;
            onFireCallback();

            if(loop)
            {
                resetTimer();
            }
        }
    }

    /*********************************** Methods ***********************************/

    public bool hasFired() { return fired; }
    public void resetTimer()
    {
        paused = false;
        fired = false;
        timeElapsed = 0.0f;
    }

    public void pauseTimer() { paused = true; }
    public void resumeTimer() {  paused = false; }
}
