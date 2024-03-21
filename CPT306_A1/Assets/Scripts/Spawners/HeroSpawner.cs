using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class HeroSpawner : LevelObjSpawner<Hero>
{

    private static readonly float minX = Map.mapMinX + .5f * Hero.width;
    private static readonly float maxX = Map.mapMaxX - .5f * Hero.width;
    private static readonly float minY = Map.mapMinY + .5f * Hero.height;
    private static readonly float maxY = Map.mapMaxY - .5f * Hero.height;

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
