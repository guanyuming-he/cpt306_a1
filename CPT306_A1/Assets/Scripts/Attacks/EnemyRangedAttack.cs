
using UnityEngine;

public class EnemyRangedAttack : RangedAttack
{
    public EnemyRangedAttack(LevelObject src, int dmg, float cd, float projSpeed, ProjSpawner projSpawner) : 
        base(src, dmg, cd, projSpeed, projSpawner) 
    {
        Debug.Assert(src is RangedEnemy);
    }

    protected override Vector2 calcProjDirection()
    {
        var hero = Game.gameSingleton.map.hero;

        // Perhaps the hero just died.
        if(hero == null)
        {
            return Vector2.up;
        }

        // If the hero is alive, then calc the direction.
        var dst = hero.getPos();
        // src is the enemy.
        var src = getSrc().getPos();
        return MovingObject.calculateDirection(src, dst);
    }
}
