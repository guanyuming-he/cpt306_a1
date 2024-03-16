using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    /*********************************** Fields ***********************************/
    protected readonly int damage;
    Timer cdTimer;
    bool inCooldown;

    public Attack(int dmg, float cd)
    {
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
    private void cdComplete()
    {
        inCooldown = false;
    }

    protected abstract void attack(float x, float y);

    public void tryAttack(float x, float y)
    {
        if (inCooldown) return;

        attack(x, y);

        inCooldown = true;
        cdTimer.resetTimer();
    }    
}
