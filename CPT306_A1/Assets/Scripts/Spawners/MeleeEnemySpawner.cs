using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemySpawner : LevelObjSpawner<MeleeEnemy>
{

    public MeleeEnemySpawner(GameObject prefab) : base(prefab) { }
    public override MeleeEnemy spawnRandom(Map map)
    {
        return base.spawnRandom(map);
    }
}