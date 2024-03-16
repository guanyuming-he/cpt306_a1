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
	/// </summary>
	/// <param name="map">the current map</param>
	/// <returns>the spawned object. never null.</returns>
	public abstract T spawnRandom(Map map);

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
}