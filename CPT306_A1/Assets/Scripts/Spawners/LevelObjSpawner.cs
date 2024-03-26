using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;

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
	/// Spawns a LevelObject of type T at a random location in the map.
    /// The location will be decided with the size of the object in mind.
    /// As LevelObject.size is the maximum one among all sizes,
    /// the default impl uses this value. (C# unfortunately doesn't support T.size).
	/// 
	/// Default impl only cares about obstacles on the map. 
    /// If won't spawn objects overlapping with obstacles.
	/// </summary>
	/// <param name="map">the current map</param>
	/// <returns>the spawned object. never null.</returns>
	public virtual T spawnRandom(Map map)
	{
        var half = .5f * LevelObject.size;
        
        // random until obj at pos is not overlapping with any obstacle.
        UnityEngine.Vector2 pos;
        do
        {
            float x = UnityEngine.Random.Range(Map.mapMinX + half.x, Map.mapMaxX - half.x);
            float y = UnityEngine.Random.Range(Map.mapMinY + half.y, Map.mapMaxY - half.y);
            pos = new UnityEngine.Vector2(x, y);
        }
        while (!locationClear(map, pos));

        return spawnAt(pos);
    }

    /// <summary>
    /// Spawns a LevelObject of type T at pos.
    /// </summary>
    /// <param name="pos"></param>
    /// <returns>the spawned object. never null.</returns>
    public virtual T spawnAt(UnityEngine.Vector2 pos)
	{
        // if any part of the object may get out of the map
        var half = .5f * LevelObject.size;
        if(pos.x < Map.mapMinX + half.x || pos.x > Map.mapMaxX - half.x)
        {
            throw new System.ArgumentException("pos out of map");
        }
        if (pos.y < Map.mapMinY + half.y || pos.y > Map.mapMaxY - half.y)
        {
            throw new System.ArgumentException("pos out of map");
        }

        var gameObj = GameObject.Instantiate(prefab, new UnityEngine.Vector3(pos.x, pos.y, 0.0f), Quaternion.identity);
		var levelObj = gameObj.GetComponent<T>();

		System.Diagnostics.Debug.Assert(levelObj != null, "I must attach the script to the prefab.");
		return levelObj;
	}

    /// <summary>
    /// The hero and enemies all have dynamic rigidbody which prevents overlapping for them.
    /// The only problem is that I can't spawn overlapping obstacles that only have static rigidbody.
    /// </summary>
    /// <param name="map"></param>
    /// <param name="pos"></param>
    /// <returns>true iff spawning a new object at pos
    /// will not result in overlapping with existing OBSTACLES.</returns>
    protected static bool locationClear(Map map, UnityEngine.Vector2 pos)
    {
        foreach (var obs in map.obstacles)
        {
            var posDiff = pos - obs.getPos();
            if (posDiff.magnitude <= Obstacle.size.magnitude)
            {
                return false;
            }
        }

        return true;
    }
}