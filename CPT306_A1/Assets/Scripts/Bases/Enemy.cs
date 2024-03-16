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

    // Must be assigned by the subclass, but not necessarily in the constructsor.
    protected Attack attack;
    public Enemy()
    {
        // so it's not dead.
        _health = 1;
    }

    public Enemy(int health)
    {
        this._health = health;
    }

    protected override void Awake()
    {
        base.Awake();

        // attack must have been assigned.
        Debug.Assert(attack != null);
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
            destroy();
        }
    }
}
