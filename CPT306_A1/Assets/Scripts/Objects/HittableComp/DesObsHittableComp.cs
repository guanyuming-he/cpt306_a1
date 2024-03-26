public sealed class DesObsHittableComp : HittableComponent
{
    public DesObsHittableComp() : base() { }

    protected override void takeDamage(int dmg, LevelObject src)
    {
        // do nothing. when it's dead, it's destroyed in DesObstacle.Update()
    }

    /// <summary>
    /// Don't forget to call base's Start.
    /// </summary>
    protected override void Start()
    {
        base.Start();
    }
}