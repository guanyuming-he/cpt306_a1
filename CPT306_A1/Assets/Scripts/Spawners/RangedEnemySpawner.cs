using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class RangedEnemySpawner : LevelObjSpawner<RangedEnemy>
{
    public RangedEnemySpawner(GameObject prefab) : base(prefab) { }

    public override RangedEnemy spawnRandom(Map map)
    {
        throw new System.NotImplementedException();
    }

}