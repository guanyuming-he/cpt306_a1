using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;

public interface ILevelObjSpawner<T> where T : LevelObject
{
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
    public abstract T spawnAt(UnityEngine.Vector2 pos);
}