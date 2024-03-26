using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class HeroRangedAttack : RangedAttack
{
    public HeroRangedAttack(LevelObject src, int dmg, float cd, float projSpeed, ProjSpawner projSpawner) 
        : base(src, dmg, cd, projSpeed, projSpawner)
    {
        System.Diagnostics.Debug.Assert(src is Hero);
    }

    protected override Vector2 calcProjDirection()
    {
        var hero = getSrc() as Hero;
        return
            (new Vector2(hero.shootingCrossbar.transform.position.x, hero.shootingCrossbar.transform.position.y)
            - hero.getPos()).normalized;
    }
}