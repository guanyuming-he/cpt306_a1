using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MovingObject
{
    // Must be assigned by the subclass, but not necessarily in the constructsor.
    protected Attack attack;
    // must be created in Awake() by AddComponent()
    protected EnemyHittableComp hittableComp;

    public static readonly float width = .8f;
    public static readonly float height = .8f;
    public static readonly float diagonal = Mathf.Sqrt(width * width + height + height);

    public Enemy() : base() {}

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
