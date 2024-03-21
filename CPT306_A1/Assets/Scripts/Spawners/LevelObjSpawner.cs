using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public abstract class LevelObjSpawner<T> where T : LevelObject
{
	// The gameobject (that contains the levelobject) to spawn.
	// Passed to the constructor by the owner of the spawner.
	protected readonly GameObject prefab;

    /// <summary>
    /// Gives the prefab to spawn to the spawner
    /// </summary>
    /// <param name="prefab">MUST NOT be null.</param>
    /// <exception cref="System.ArgumentNullException"></exception>
    public LevelObjSpawner(in GameObject prefab)
	{
		if(prefab == null)
		{
			throw new System.ArgumentNullException();
		}

		this.prefab = prefab;
	}

	/// <summary>
	/// Spawns a LevelObject of type T at a random location (impl specified)
	/// in the map.
	/// 
	/// Default impl does not care about what's on the map.
	/// It just spawns the prefab randomly.
	/// </summary>
	/// <param name="map">the current map</param>
	/// <returns>the spawned object. never null.</returns>
	public virtual T spawnRandom(Map map)
	{
        float x = UnityEngine.Random.Range(Map.mapMinX, Map.mapMaxX);
		float y = UnityEngine.Random.Range(Map.mapMinY, Map.mapMaxY);
		return spawnAt(new UnityEngine.Vector2(x, y));
    }

    /// <summary>
    /// Spawns a LevelObject of type T at pos.
    /// </summary>
    /// <param name="pos"></param>
    /// <returns>the spawned object. never null.</returns>
    public virtual T spawnAt(UnityEngine.Vector2 pos)
	{
		var gameObj = GameObject.Instantiate(prefab);
		var levelObj = gameObj.GetComponent<T>();

		Debug.Assert(levelObj != null, "I must attach the script to the prefab.");
		levelObj.setPos(pos);
		return levelObj;
	}

    /// <param name="map"></param>
    /// <param name="pos"></param>
    /// <returns>true iff spawning a new object at pos
    /// will not result in overlapping with existing OBSTACLES.</returns>
    protected static bool locationClear(Map map, UnityEngine.Vector2 pos)
    {
        foreach (var obs in map.obstacles)
        {
            var posDiff = pos - obs.getPos();
            if (posDiff.magnitude <= Obstacle.diagonal)
            {
                return false;
            }
        }

        return true;
    }
}