using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MovingObject
{
    // Must be assigned by the subclass, but not necessarily in the constructsor.
    protected Attack attack;
    // must be created in Awake() by AddComponent()
    protected EnemyHittableComp hittableComp;

    public static new readonly Vector2 size = new Vector2(.8f, .8f);

    public Enemy() : base() {}

    /// <summary>
    /// Cap enemies inside the map
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    protected override Vector2 nextPos(float dt)
    {
        var next = base.nextPos(dt);

        // cap next inside map
        // use LevelObject's size instead because inside spawner I can't
        // use T's but to use base's
        var half = .5f * LevelObject.size;
        next.x = Mathf.Max(Map.mapMinX + half.x, next.x);
        next.x = Mathf.Min(Map.mapMaxX - half.x, next.x);
        next.y = Mathf.Max(Map.mapMinY + half.y, next.y);
        next.y = Mathf.Min(Map.mapMaxY - half.y, next.y);

        return next;
    }


    protected override void Awake()
    {
        base.Awake();

        // attack must have been assigned.
        System.Diagnostics.Debug.Assert(attack != null);
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        var hittable = gameObject.GetComponent<IHittable>();
        if (hittable.dead())
        {
            destroy();
            return;
        }

        // don't forget to update attack
        attack.update(Time.deltaTime);

        base.Update();
    }
}
