public sealed class EnemyHittableComp : HittableComponent
{
    public EnemyHittableComp() : base() { }

    protected override void takeDamage(int dmg, LevelObject src)
    {
        throw new System.NotImplementedException("if dead, then add scores.");
    }

    /// <summary>
    /// Don't forget to call base's Start.
    /// </summary>
    protected override void Start()
    {
        base.Start();
    }
}