using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelObject : MonoBehaviour
{
    // no level object is bigger than this size.
    public static readonly Vector2 size = Vector2.one;

    /*********************************** MonoBehaviour ***********************************/
    // Declare them as virtual to ensure correct init/update order.
    protected virtual void Awake()
    {
        // Do nothing1
    }

    protected virtual void Start()
    {
        // Do nothing.
    }

    protected virtual void Update()
    {
        if (!Game.gameSingleton.running())
        {
            return;
        }
        // Do nothing.
    }

    /*********************************** Methods ***********************************/
    public void setPos(Vector2 pos)
    {
        gameObject.transform.position =
            new Vector3(pos.x, pos.y, gameObject.transform.position.z);
    }

    public Vector2 getPos()
    {
        return new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
    }

    /// <summary>
    /// Destroys self and all objects it has created.
    /// </summary>
    public virtual void destroy()
    {
        UnityEngine.Object.Destroy(gameObject);
    }
}
