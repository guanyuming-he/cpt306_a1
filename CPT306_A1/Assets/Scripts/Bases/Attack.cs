using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Attack : MonoBehaviour
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
        if(src == null)
        {
            throw new System.ArgumentNullException();
        }

        this.src = src;
        damage = dmg;
        cdTimer = new Timer(cd, cdComplete);
        inCooldown = false;
    }

    /*********************************** MonoBehaviour ***********************************/

    // Update is called once per frame
    void Update()
    {
        cdTimer.update(Time.deltaTime);
    }

    /*********************************** Methods ***********************************/

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
