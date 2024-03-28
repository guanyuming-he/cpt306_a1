using Unity.VisualScripting;

/// <summary>
/// Destructable Object
/// </summary>
public sealed class DesObstacle : Obstacle
{
    private DesObsHittableComp hittableComp;

    protected override void Awake()
    {
        base.Awake();

        // create the hittable comp
        hittableComp = gameObject.AddComponent<DesObsHittableComp>();
        // specified, p.2
        hittableComp.initHealth(3);
    }

    protected override void Update()
    {
        if (!Game.gameSingleton.running())
        {
            return;
        }

        var hittable = gameObject.GetComponent<IHittable>();
        if(hittable.dead())
        {
            destroy();
            return;
        }

        base.Update();
    }
}