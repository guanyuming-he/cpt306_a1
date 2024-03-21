using UnityEngine;

public sealed class HeroMeleeAttack : MeleeAttack
{
    public HeroMeleeAttack(LevelObject src, int dmg, float cd, Vector2 attackRange)
        : base(src, dmg, cd, attackRange) { }

    protected override bool canHit(GameObject obj)
    {
        var levelObj = obj.GetComponent<LevelObject>();
        if (levelObj == null)
        {
            return false;
        }

        // hero can only hit enemies and destructable obstacles.
        return levelObj is Enemy || levelObj is DesObstacle;
    }
}