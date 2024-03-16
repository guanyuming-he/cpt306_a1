using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : LevelObject
{
    /*********************************** Fields ***********************************/
    public float speed;
    public Vector2 direction;

    public MovingObject() : this(0.0f, Vector2.up) { }

    public MovingObject(float speed, Vector2 direction)
    {
        this.speed = speed;
        this.direction = direction;
    }

    /*********************************** MonoBehaviour ***********************************/
    // Start is called before the first frame update


    protected override void Update()
    {
        base.Update();

        rigidBody.SetRotation(directionToRotationAngle(direction));
        rigidBody.velocity = speed * direction;
    }

    /*********************************** static helpers ***********************************/
    /// <summary>
    /// 
    /// </summary>
    /// <param name="src"></param>
    /// <param name="dst"></param>
    /// <returns>normalized dst - src, or up if dst == src </returns>
    public static Vector2 calculateDirection(in Vector2 src, in Vector2 dst)
    {
        if(dst == src)
        {
            return Vector2.up;
        }

        return (dst - src).normalized;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dir">assumed to be normalized</param>
    /// <returns>the rotation angle (in degrees) represented by the direction</returns>
    public static float directionToRotationAngle(in Vector2 dir)
    {
        Debug.Assert(dir.x != 0.0f);

        // magnitude = 1, so x = cos.
        return Mathf.Acos(dir.x) * Mathf.Rad2Deg;
    }
}
