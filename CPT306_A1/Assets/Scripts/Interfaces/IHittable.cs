using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public interface IHittable
{
    /*********************************** fields ***********************************/
    protected abstract int health 
    {
        get; set; 
    }

    /*********************************** Methods ***********************************/
    public bool dead() { return health <= 0; }

    /// <summary>
    /// Called when src deals dmg damage to this.
    /// </summary>
    /// <param name="dmg"></param>
    /// <param name="src">can be null (e.g. the src has already died)</param>
    public void onHit(int dmg, LevelObject src)
    {
        health -= dmg;
        takeDamage(dmg, src);
    }

    protected virtual void takeDamage(int dmg, LevelObject src)
    {

    }
}
