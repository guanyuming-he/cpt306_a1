using JetBrains.Annotations;
using System;
using UnityEngine;

/// <summary>
/// Abstract as hero and fire projectile in different ways
/// </summary>
public abstract class RangedAttack : Attack
{
    public readonly ProjSpawner projSpawner;
    private readonly float projSpeed;

    /// <summary>
    /// Must be called when the projSpawner is available.
    /// Usually it's later than or equal to Awake()
    /// </summary>
    /// <param name="src"></param>
    /// <param name="dmg"></param>
    /// <param name="cd"></param>
    /// <param name="projSpeed"></param>
    /// <param name="projSpawner"></param>
    public RangedAttack
    (
        LevelObject src,
        int dmg, float cd,
        float projSpeed,
        ProjSpawner projSpawner
    ) : base(src, dmg, cd) 
    {
        this.projSpeed = projSpeed;
        this.projSpawner = projSpawner;
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
