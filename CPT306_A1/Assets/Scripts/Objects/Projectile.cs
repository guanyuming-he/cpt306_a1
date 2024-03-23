using System.Diagnostics;
using System.Numerics;

public sealed class Projectile : MovingObject
{
    public int dmg;
    public LevelObject src;

    public static new readonly UnityEngine.Vector2 size = new UnityEngine.Vector2(.5f, .5f);

    public Projectile() : base() {}

    /// <summary>
    /// destroy the projectile if it goes out of map
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    protected override UnityEngine.Vector2 nextPos(float dt)
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

    protected override void Awake()
    {
        base.Awake();

        // I assume that dmg is set by RangedAttack.attack()
        Debug.Assert(dmg > 0);

        // src is instead set by the spawner.
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
}
