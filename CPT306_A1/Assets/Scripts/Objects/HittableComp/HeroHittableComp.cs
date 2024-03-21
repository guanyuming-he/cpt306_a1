public sealed class HeroHittableComp : HittableComponent
{
    public HeroHittableComp() : base() {}

    protected override void takeDamage(int dmg, LevelObject src)
    {
        // Do nothing.
        // reaction to the hero's death is handled in Game.
    }

    /// <summary>
    /// Don't forget to call base's Start.
    /// </summary>
    protected override void Start()
    {
        base.Start();
    }
}