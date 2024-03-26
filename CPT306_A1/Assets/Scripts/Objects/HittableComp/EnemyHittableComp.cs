using System.Diagnostics;

public sealed class EnemyHittableComp : HittableComponent
{
    public EnemyHittableComp() : base() { }

    protected override void takeDamage(int dmg, LevelObject src)
    {
        System.Diagnostics.Debug.Assert(src is Hero, "only the hero can deal damage to an emeny.");

        if(dead())
        {
            // melee +10
            // ranged +20
            var me = gameObject.GetComponent<Enemy>();
            System.Diagnostics.Debug.Assert(me != null);
            if (me is MeleeEnemy)
            {
                Game.gameSingleton.stateMgr.addScore(10);
            }
            else if (me is RangedEnemy)
            {
                Game.gameSingleton.stateMgr.addScore(20);
            }
        }
    }

    /// <summary>
    /// Don't forget to call base's Start.
    /// </summary>
    protected override void Start()
    {
        base.Start();
    }
}