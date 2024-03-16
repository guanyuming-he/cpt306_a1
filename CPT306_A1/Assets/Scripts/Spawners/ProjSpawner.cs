using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ProjSpawner : MonoBehaviour, ILevelObjSpawner<Projectile>
{
    public Projectile spawnAt(Vector2 pos)
    {
        throw new NotImplementedException();
    }

    public Projectile spawnRandom(Map map)
    {
        throw new System.NotImplementedException();
    }

}