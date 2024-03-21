using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class DesObsSpawner : LevelObjSpawner<DesObstacle>
{
    public DesObsSpawner(GameObject prefab) : base(prefab) { }

    private static readonly float minX = Map.mapMinX + .5f * Obstacle.width;
    private static readonly float maxX = Map.mapMaxX - .5f * Obstacle.width;
    private static readonly float minY = Map.mapMinY + .5f * Obstacle.height;
    private static readonly float maxY = Map.mapMaxY - .5f * Obstacle.height;

    public override DesObstacle spawnRandom(Map map)
    {
        float x, y;
        Vector2 pos;
        do
        {
            x = UnityEngine.Random.Range(minX, maxX);
            y = UnityEngine.Random.Range(minY, maxY);
            pos = new Vector2(x, y);
        }
        while (!locationClear(map, pos));

        return spawnAt(pos);
    }
}

