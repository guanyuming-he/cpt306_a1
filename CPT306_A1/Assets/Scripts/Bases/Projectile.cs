using System.Diagnostics;
using System.Numerics;

public abstract class Projectile : MovingObject
{
    // set by the one who owns the spawner
    public int dmg;
    public LevelObject src;

    public static new readonly UnityEngine.Vector2 size = new UnityEngine.Vector2(.5f, .5f);

    public Projectile() : base() {}

    /// <summary>
    /// destroy the projectile if it goes out of map
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    protected sealed override UnityEngine.Vector2 nextPos(float dt)
    {
        var next = base.nextPos(dt);

        // destroy the projectile if it goes out of map
        if(next.x < Map.mapMinX + size.x || next.x > Map.mapMaxX - size.y)
        {
            destroy();
        }
        if (next.y < Map.mapMinY + size.y || next.y > Map.mapMaxY - size.y)
        {
            destroy();
        }

        return next;
    }

    protected sealed override void Awake()
    {
        base.Awake();
    }

    protected sealed override void Start()
    {
        base.Start();

        // I assume that dmg is set by RangedAttack.attack()
        Game.MyDebugAssert(dmg > 0);
    }

    protected sealed override void Update()
    {
        if (!Game.gameSingleton.running())
        {
            return;
        }

        base.Update();
    }

    protected virtual void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        // do nothing.
        // derived class must do something
    }
}
