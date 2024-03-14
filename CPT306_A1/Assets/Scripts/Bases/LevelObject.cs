using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelObject : MonoBehaviour
{
    protected Rigidbody2D rigidBody = null;

    // Declare it as virtual to ensure correct init order.
    protected virtual void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        Debug.Assert(rigidBody != null, "Did I forget to assign a rigidbody to this prefab?");
    }

    public void setPos(float x, float y)
    {
        transform.position =
            new Vector3(x, y, transform.position.z);
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
