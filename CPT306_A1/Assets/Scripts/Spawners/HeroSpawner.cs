using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class HeroSpawner : LevelObjSpawner<Hero>
{
    public HeroSpawner(GameObject prefab) : base(prefab) { }

    /// <summary>
    /// Randomly spawns the hero at any place in the map
    /// </summary>
    /// <param name="map"></param>
    /// <returns></returns>
    public override Hero spawnRandom(Map map)
    {
        return base.spawnRandom(map);
    }

}
