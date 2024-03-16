using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemySpawner : LevelObjSpawner<MeleeEnemy>
{

    public MeleeEnemySpawner(GameObject prefab) : base(prefab) { }
    public override MeleeEnemy spawnRandom(Map map)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
