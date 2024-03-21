using UnityEngine;

public sealed class EnemyMeleeAttack : MeleeAttack
{
    public EnemyMeleeAttack(LevelObject src, int dmg, float cd, Vector2 attackRange)
        : base(src, dmg, cd, attackRange) { }

    protected override bool canHit(GameObject obj)
    {
        var levelObj = obj.GetComponent<LevelObject>();
        if (levelObj == null)
        {
            return false;
        }

        // enemy can only hit the hero and destructable obstacles.
        return levelObj is Hero || levelObj is DesObstacle;
    }
}