using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSpawner : MonoBehaviour, ILevelObjSpawner<Hero>
{
    public Hero spawnAt(Vector2 pos)
    {
        throw new System.NotImplementedException();
    }

    public Hero spawnRandom(Map map)
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
