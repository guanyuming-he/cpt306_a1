using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelObject : MonoBehaviour
{
    /*********************************** Fields ***********************************/
    protected Rigidbody2D rigidBody = null;

    /*********************************** MonoBehaviour ***********************************/
    // Declare them as virtual to ensure correct init/update order.
    protected virtual void Awake()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        Debug.Assert(rigidBody != null, "Did I forget to assign a rigidbody to this prefab?");
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
