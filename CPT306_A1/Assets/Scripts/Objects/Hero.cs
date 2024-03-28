using System;
using UnityEngine;

public class Hero : MovingObject
{
    /*********************************** Fields ***********************************/

    HeroHittableComp hittableComp;

    // Created in Awake where prefabs are available
    private MeleeAttack meleeAttack;
    private RangedAttack rangedAttack;

    // Assigned in the editor, instantiated in Awake()
    // location updated to mouse in Update().
    public GameObject shootingCrossbar;

    // assigned in the editor
    public GameObject bulletPrefab;
    // created in Awake()
    private static ProjSpawner bulletSpawner = null;

    public static new readonly Vector2 size = new Vector2(.8f, .8f);

    /*********************************** Ctor ***********************************/
    public Hero() : base() 
    {
        // specified.
        speed = 2.5f;
        // can create the melee attack here.
        // but for consistency create it in Awake() instead.
    }

    /*********************************** Settings ***********************************/
    public static readonly Vector2 meleeAttackRange = new Vector2(3.0f, 3.0f);

    /*********************************** Methods ***********************************/
    public Attack getMeleeAttack() { return meleeAttack; }
    public Attack getRangedAttack() { return rangedAttack; }

    /// <summary>
    /// Cap the hero inside the map
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    protected override Vector2 nextPos(float dt)
    {
        var next = base.nextPos(dt);

        // cap next inside map
        // use LevelObject's size instead because inside spawner I can't
        // use T's but to use base's
        var half = .5f * LevelObject.size;
        next.x = Mathf.Max(Map.mapMinX + half.x, next.x);
        next.x = Mathf.Min(Map.mapMaxX - half.x, next.x);
        next.y = Mathf.Max(Map.mapMinY + half.y, next.y);
        next.y = Mathf.Min(Map.mapMaxY - half.y, next.y);

        return next;
    }

    public override void destroy()
    {
        // Don't forget to destroy the crossbar
        GameObject.Destroy(shootingCrossbar);
        base.destroy();
    }

    /*********************************** MonoBehaviour ***********************************/
    protected override void Awake()
    {
        base.Awake();

        // instantiate the shooting crossbar at the center of the screen.
        shootingCrossbar = GameObject.Instantiate(shootingCrossbar, Vector3.zero, Quaternion.identity);

        // create the bullet spawner, if not created yet.
        if(bulletSpawner == null)
        {
            // assigned in the editor
            Game.MyDebugAssert(bulletPrefab != null);
            bulletSpawner = new ProjSpawner(bulletPrefab);
        }

        // create the hittable comp
        hittableComp = gameObject.AddComponent<HeroHittableComp>();
        // specified
        hittableComp.initHealth(30);

        // Create the attacks
        meleeAttack = new HeroMeleeAttack(this, 2, 2.0f, meleeAttackRange);
        rangedAttack = new HeroRangedAttack(this, 1, 0.5f, 2.0f, bulletSpawner);
    }

    // Update is called once per frame
    protected override void Update()
    {
        if(!Game.gameSingleton.running())
        {
            return;
        }

        var hittable = gameObject.GetComponent<IHittable>();
        if (hittable.dead())
        {
            // Do nothing here.
            // It is the Game's responsibility to react to this event.
            return;
        }

        // 1. update attacks
        // 2. update shooting crossbar
        // 3. response to user inputs

        // 1.
        {
            rangedAttack.update(Time.deltaTime);
            meleeAttack.update(Time.deltaTime);
        }

        // 2.
        {
            var mouseScreenPos = Input.mousePosition;
            var mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
            shootingCrossbar.transform.position = new Vector3(mouseWorldPos.x, mouseWorldPos.y, 0.0f);
        }

        // 3.
        {
            // Respond to direction keys
            var dirSum = Vector2.zero;
            if (Input.GetKey(KeyCode.W)) dirSum += Vector2.up;
            if (Input.GetKey(KeyCode.S)) dirSum += Vector2.down;
            if (Input.GetKey(KeyCode.A)) dirSum += Vector2.left;
            if (Input.GetKey(KeyCode.D)) dirSum += Vector2.right;
            direction = dirSum.normalized;

            // base updates the location using direction and speed.
            // do this immediately after calculating direction.
            base.Update();

            // Respond to attack keys
            // LMB
            if (Input.GetMouseButton(0))
            {
                rangedAttack.tryAttack(getPos());
            }
            // RMB
            if (Input.GetMouseButton(1))
            {
                meleeAttack.tryAttack(getPos());
            }
        }
    }
}
