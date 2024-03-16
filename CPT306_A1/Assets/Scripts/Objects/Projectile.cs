

using System.Diagnostics;

public sealed class Projectile : MovingObject
{
    public int dmg;
    public LevelObject src;

    public Projectile() : base() {}

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
