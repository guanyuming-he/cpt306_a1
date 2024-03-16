/// <summary>
/// Destructable Object
/// </summary>
public class DesObstacle : Obstacle, IHittable
{
    private int _health;
    int IHittable.health
    {
        get { return _health; }
        set { _health = value; }
    }
}