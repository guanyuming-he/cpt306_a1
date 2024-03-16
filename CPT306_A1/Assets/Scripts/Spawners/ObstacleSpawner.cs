using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner :LevelObjSpawner<Obstacle>
{

    public ObstacleSpawner(GameObject prefab) : base(prefab) { }
    public override Obstacle spawnRandom(Map map)
    {
        throw new System.NotImplementedException();
    }

}
