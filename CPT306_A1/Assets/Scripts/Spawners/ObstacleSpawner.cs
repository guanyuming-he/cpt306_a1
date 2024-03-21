using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner :LevelObjSpawner<Obstacle>
{
    public ObstacleSpawner(GameObject prefab) : base(prefab) { }

    private static readonly float minX = Map.mapMinX + .5f * Obstacle.width;
    private static readonly float maxX = Map.mapMaxX - .5f * Obstacle.width;
    private static readonly float minY = Map.mapMinY + .5f * Obstacle.height;
    private static readonly float maxY = Map.mapMaxY - .5f * Obstacle.height;

    public override Obstacle spawnRandom(Map map)
    {
        float x, y;
        Vector2 pos;
        do
        {
            x = Random.Range(minX, maxX);
            y = Random.Range(minY, maxY);
            pos = new Vector2(x, y);
        }
        while (!locationClear(map, pos));

        return spawnAt(pos);
    }
}
