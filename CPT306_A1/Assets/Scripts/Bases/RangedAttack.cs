using System;
using UnityEngine;

/// <summary>
/// Abstract as hero and fire projectile in different ways
/// </summary>
public abstract class RangedAttack : Attack
{
    // Assigned by the owning script later than in the ctor
    // as the prefab won't be available then.
    public ProjSpawner projSpawner;
    private readonly float projSpeed;

    public RangedAttack
    (
        LevelObject src,
        int dmg, float cd,
        float projSpeed
    ) : base(src, dmg, cd) 
    {
        this.projSpeed = projSpeed;
    }

    protected override void attack(Vector2 pos)
    {
        var proj = projSpawner.spawnAt(pos);

        proj.dmg = getDmg();
        proj.src = getSrc();
        proj.speed = projSpeed;
        proj.direction = calcProjDirection();
    }

    protected abstract Vector2 calcProjDirection();
}
