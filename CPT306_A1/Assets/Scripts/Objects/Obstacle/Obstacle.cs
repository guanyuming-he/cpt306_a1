using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : LevelObject
{
    public static new readonly Vector2 size = Vector2.one;

    public Obstacle() : base() { }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }
}
