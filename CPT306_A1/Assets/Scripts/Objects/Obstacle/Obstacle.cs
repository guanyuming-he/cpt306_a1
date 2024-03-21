using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : LevelObject
{
    public static readonly float width = 1.0f;
    public static readonly float height = 1.0f;
    public static readonly float diagonal = Mathf.Sqrt(width * width + height + height);

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }
}
