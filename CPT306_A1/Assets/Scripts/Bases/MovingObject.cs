using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : LevelObject
{
    /*********************************** Fields ***********************************/
    protected float speed;
    protected Vector2 direction;

    public MovingObject()
    {
        speed = 0.0f;
        direction = Vector2.right;
    }

    /*********************************** MonoBehaviour ***********************************/
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }
}
