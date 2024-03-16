using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSpawner : LevelObjSpawner<Hero>
{
    public HeroSpawner(GameObject prefab) : base(prefab) { }

    public override Hero spawnRandom(Map map)
    {
        throw new System.NotImplementedException();
    }

}
