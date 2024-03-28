using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : LevelObject
{
    /*********************************** Fields ***********************************/
    public float speed;
    public Vector2 direction;

    // null if I do not have one
    protected Rigidbody2D rigidbody2d;

    public MovingObject() : this(0.0f, Vector2.up) { }

    public MovingObject(float speed, Vector2 direction)
    {
        this.speed = speed;
        this.direction = direction;
    }

    /*********************************** Methods ***********************************/
    /// <summary>
    /// Returns the position next frame.
    /// Hero and enemy override this to cap themselves inside map.
    /// Projectiles use this to destroy themselves when nextPos would result in outside of the map.
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    protected virtual Vector2 nextPos(float dt)
    {
        return getPos() + dt * speed * direction;
    }

    /*********************************** MonoBehaviour ***********************************/

    protected override void Start()
    {
        base.Start();

        // If I have a rigidBody, then use it
        rigidbody2d = gameObject.GetComponent<Rigidbody2D>();
    }

    protected override void Update()
    {
        base.Update();
     
        // if I have a rigidBody, then move the rigidbody instead
        if(rigidbody2d != null)
        {
            rigidbody2d.transform.rotation = Quaternion.Euler(0, 0, directionToRotationAngle(direction));
            rigidbody2d.transform.position = nextPos(Time.deltaTime);
        }
        // Otherwise, use my own transform
        else
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, directionToRotationAngle(direction));
            gameObject.transform.position = nextPos(Time.deltaTime);
        }
    }

    /*********************************** static helpers ***********************************/

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

    /// <param name="dir">assumed to be normalized</param>
    /// <returns>the rotation angle (in degrees) represented by the direction</returns>
    public static float directionToRotationAngle(in Vector2 dir)
    {
        // magnitude = 1, so x = cos.
        return Mathf.Acos(dir.x) * Mathf.Rad2Deg + 90.0f;
    }
}
