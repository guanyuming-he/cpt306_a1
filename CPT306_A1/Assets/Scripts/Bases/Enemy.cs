using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingObject, IHittable
{
    private int _health;
    int IHittable.health
    {
        get { return _health; }
        set { _health = value; }
    }

    protected Attack attack;

    /// <summary>
    /// Spawns the enemy with health at (x,y)
    /// with the attack method
    /// </summary>
    /// <param name="health"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public Enemy(int health, float x, float y, Attack attack)
    {
        this._health = health;
        setPos(x, y);
        this.attack = attack;
    }

    // Start is called before the first frame update

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if((this as IHittable).dead())
        {
            destroy();
        }
    }
}
