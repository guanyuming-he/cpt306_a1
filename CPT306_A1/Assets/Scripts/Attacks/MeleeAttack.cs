using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public sealed class MeleeAttack : Attack
{
    private readonly Vector2 attackRange;

    public MeleeAttack
    (
        LevelObject src,
        int dmg, float cd,
        Vector2 attackRange
    ) : base(src, dmg, cd)
    {
        this.attackRange = attackRange;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>Use Physics2D.BoxCast()</remarks>
    /// <exception cref="NotImplementedException"></exception>
    protected override void attack(Vector2 pos)
    {
        throw new NotImplementedException();
    }
}
