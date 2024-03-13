using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObject : MonoBehaviour
{
    Rigidbody2D rigidBody = null;

    // Declare it as virtual to ensure correct init order.
    protected virtual void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        Debug.Assert(rigidBody != null, "Did I forget to assign a rigidbody to this prefab?");
    }
}
