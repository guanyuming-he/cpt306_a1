using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Timeline;

public abstract class MeleeAttack : Attack
{
    private readonly Vector2 attackRange;

    public MeleeAttack
    (
        LevelObject src,
        int dmg, float cd,
        Vector2 attackRange
    ) : base(src, dmg, cd)
    {
        this.attackRange = attackRange;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>Use Physics2D.BoxCast()</remarks>
    protected override void attack(Vector2 pos)
    {
        // TODO: add attack effect.
        drawDebugBox2D(pos, attackRange, Color.cyan);

        // the list of all objects (with a collider) that are in the range of the attack
        var collidedList = Physics2D.BoxCastAll
        (
            // position of the center of the box, and the half size
            pos, .5f * attackRange,
            // rotation angle of the box
            0.0f, 
            // should the cast move the box along a direction?
            // the direction, and the move distance.
            Vector2.up, 0.0f
        );

        foreach(var c in collidedList)
        {
            var obj = c.collider.gameObject;

            // If hittable is a component of the game object
            var hittable = obj.GetComponent<HittableComponent>();
            if(hittable != null)
            {
                if(canHit(obj))
                {
                    hittable.onHit(damage, getSrc());
                }
            }
        }
    }

    /// <param name="obj"></param>
    /// <returns>true iff the melee attack can hit such a GameObject as obj </returns>
    protected abstract bool canHit(GameObject obj);

    private static void drawDebugBox2D(Vector2 pos, Vector2 size, Color color, float duration = .1f)
    {
        Debug.DrawLine
        (
            new Vector3(pos.x - .5f * size.x, pos.y - .5f * size.y, 0.0f), 
            new Vector3(pos.x - .5f * size.x, pos.y + .5f * size.y, 0.0f),
            color, duration
        );
        Debug.DrawLine
        (
            new Vector3(pos.x - .5f * size.x, pos.y - .5f * size.y, 0.0f), 
            new Vector3(pos.x + .5f * size.x, pos.y - .5f * size.y, 0.0f),
            color, duration
        );
        Debug.DrawLine
        (
            new Vector3(pos.x - .5f * size.x, pos.y + .5f * size.y, 0.0f), 
            new Vector3(pos.x + .5f * size.x, pos.y + .5f * size.y, 0.0f),
            color, duration
        );
        Debug.DrawLine
        (
            new Vector3(pos.x + .5f * size.x, pos.y - .5f * size.y, 0.0f), 
            new Vector3(pos.x + .5f * size.x, pos.y + .5f * size.y, 0.0f),
            color, duration
        );
    }    
}
