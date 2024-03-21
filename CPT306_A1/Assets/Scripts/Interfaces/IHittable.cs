using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public interface IHittable
{
    /*********************************** Methods ***********************************/
    public abstract bool dead();

    /// <summary>
    /// Called when src deals dmg damage to this.
    /// </summary>
    /// <param name="dmg"></param>
    /// <param name="src">can be null (e.g. the src has already died)</param>
    public abstract void onHit(int dmg, LevelObject src);
}
