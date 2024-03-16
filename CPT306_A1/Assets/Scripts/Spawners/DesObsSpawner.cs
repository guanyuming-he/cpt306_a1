using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class DesObsSpawner : LevelObjSpawner<DesObstacle>
{
    public DesObsSpawner(GameObject prefab) : base(prefab) { }

    public override DesObstacle spawnRandom(Map map)
    {
        throw new NotImplementedException();
    }
}

