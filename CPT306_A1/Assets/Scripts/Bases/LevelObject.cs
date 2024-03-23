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
        // Do nothing.
    }

    /*********************************** Methods ***********************************/
    public void setPos(Vector2 pos)
    {
        transform.position =
            new Vector3(pos.x, pos.y, transform.position.z);
    }

    public Vector2 getPos()
    {
        return new Vector2(transform.position.x, transform.position.y);
    }

    public void destroy()
    {
        UnityEngine.Object.Destroy(gameObject);
    }
}
