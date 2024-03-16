using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MovingObject, IHittable
{
    private int _health;

    int IHittable.health
    {
        get { return _health; }
        set { _health = value; }
    }

    /// <summary>
    /// Spawns the hero with health at (x,y)
    /// with its attack methods
    /// </summary>
    /// <param name="health"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public Hero
    (
        int health,
        float x, float y,
        RangedAttack ranged, MeleeAttack melee
    )
    {
        this._health = health;
        setPos(x, y);
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if((this as IHittable).dead())
        {
            // stateMgr.gameOver();
            destroy();
            throw new NotImplementedException();
        }
    }
}
