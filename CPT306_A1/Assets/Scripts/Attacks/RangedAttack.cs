using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : Attack
{
    private ProjSpawner projSpawner;

    public RangedAttack
    (
        int dmg, float cd,
        ProjSpawner projSpawner
    ) : base(dmg, cd)
    {
        this.projSpawner = projSpawner;
    }

    protected override void attack(float x, float y)
    {
        throw new NotImplementedException();
    }
}
