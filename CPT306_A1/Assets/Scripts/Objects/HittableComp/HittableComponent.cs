
using System;
using UnityEngine;

public abstract class HittableComponent : MonoBehaviour, IHittable
{
    /*********************************** Fields ***********************************/
    // If health has been set by calling setHealth() after construction.
    // checked in Start()
    bool healthSet = false;
    private int health;

    /*********************************** Ctor ***********************************/
    /// <summary>
    /// health is constructed as 0.
    /// Set later in the containing LevelObject
    /// </summary>
    public HittableComponent()
    {
        health = 0;
    }

    /*********************************** Methods ***********************************/
    /// <summary>
    /// Init the health after default construction.
    /// </summary>
    /// <param name="startHealth"></param>
    /// <exception cref="InvalidOperationException">health can only be set once.</exception>
    /// <exception cref="ArgumentException">startHealth MUST be > 0.</exception>
    public void setHealth(int startHealth)
    {
        if (healthSet)
        {
            throw new InvalidOperationException("health can only be set once.");
        }
        // do not allow creation of dead objects.
        if (startHealth <= 0)
        {
            throw new ArgumentException("startHealth MUST be > 0.");
        }

        healthSet = true;
        health = startHealth;
    }

    public bool dead() { return health <= 0; }

    /// <summary>
    /// 1. health -= dmg
    /// 2. call takeDamamge to handle side effects of taking this damage.
    /// </summary>
    /// <param name="dmg"></param>
    /// <param name="src"></param>
    public void onHit(int dmg, LevelObject src)
    {
        health -= dmg;
        takeDamage(dmg, src);
    }

    /// <summary>
    /// Side effects of taking the dmg from src.
    /// That is, all effects that are not related to health.
    /// </summary>
    /// <param name="dmg"></param>
    /// <param name="src"></param>
    protected abstract void takeDamage(int dmg, LevelObject src);

    /*********************************** MonoBehaviour ***********************************/
    protected virtual void Start()
    {
        System.Diagnostics.Debug.Assert(healthSet, "I must set the health");
    }

}