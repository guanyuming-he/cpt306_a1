using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ProjSpawner : LevelObjSpawner<Projectile>
{
    public ProjSpawner(GameObject prefab) : base(prefab) {}

    /// <summary>
    /// </summary>
    /// <exception cref="System.NotSupportedException"></exception>
    public override Projectile spawnRandom(Map map)
    {
        Game.MyDebugAssert(false, "Not supported");
        throw new NotSupportedException();
    }

    /// <summary>
    /// Spawn a projectile at pos and sets its dmg and src 
    /// (direction MUST be set by the spawner's owner)
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public override Projectile spawnAt(Vector2 pos)
    {
        var ret =  base.spawnAt(pos);

        // src, dmg, direction, and speed 
        // are ALL set by the Attack
        // For now nothing to do here.

        return ret;
    }

}