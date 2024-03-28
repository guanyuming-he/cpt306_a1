using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Attack
{
    /*********************************** Fields ***********************************/
    // An attack must be owned by a LO.
    private readonly LevelObject src;
    protected readonly int damage;
    Timer cdTimer;
    bool inCooldown;

    // Attack can be created purely in script and not attacked directly to a GO.
    // Hence I can use some parameters.
    public Attack(LevelObject src, int dmg, float cd)
    {
        Game.MyDebugAssert(src != null, "Attack must be owned by someone. Usually it's set by the owning object.");

        this.src = src;
        damage = dmg;
        cdTimer = new Timer(cd, cdComplete);
        inCooldown = false;
    }

    /*********************************** Methods ***********************************/

    public bool inCd() { return inCooldown; }
    public Timer getCdTimer() { return cdTimer; }

    /// <summary>
    /// must be called in containing LevelObject's Update()
    /// </summary>
    /// <param name="dt"></param>
    public void update(float dt)
    {
        if (!Game.gameSingleton.running())
        {
            return;
        }
        cdTimer.update(dt);
    }

    public LevelObject getSrc() { return src; }
    public int getDmg() { return damage; }
    private void cdComplete()
    {
        inCooldown = false;
    }

    protected abstract void attack(Vector2 pos);

    public void tryAttack(Vector2 pos)
    {
        if (inCooldown) return;

        attack(pos);

        inCooldown = true;
        cdTimer.resetTimer();
    }    
}
