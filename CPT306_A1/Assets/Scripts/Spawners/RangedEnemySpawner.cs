using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class RangedEnemySpawner : MonoBehaviour, ILevelObjSpawner<RangedEnemy>
{
    public RangedEnemy spawnAt(Vector2 pos)
    {
        throw new NotImplementedException();
    }

    public RangedEnemy spawnRandom(Map map)
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