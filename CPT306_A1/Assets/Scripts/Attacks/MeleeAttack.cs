using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

public sealed class MeleeAttack : Attack
{
    private readonly Vector2 attackRange;

    public MeleeAttack
    (
        int dmg, float cd,
        Vector2 attackRange
    ) : base(dmg, cd)
    {
        this.attackRange = attackRange;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <remarks>Use Physics2D.BoxCast()</remarks>
    /// <exception cref="NotImplementedException"></exception>
    protected override void attack(float x, float y)
    {
        throw new NotImplementedException();
    }
}
