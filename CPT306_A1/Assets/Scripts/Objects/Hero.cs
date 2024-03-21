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

    public static readonly float width = .8f;
    public static readonly float height = .8f;
    public static readonly float diagonal = Mathf.Sqrt(width * width + height + height);

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
            Debug.Assert(bulletPrefab != null);
            bulletSpawner = new ProjSpawner(bulletPrefab);
        }

        // create the hittable comp
        hittableComp = gameObject.AddComponent<HeroHittableComp>();
        // specified
        hittableComp.setHealth(30);

        // Create the attacks
        meleeAttack = new HeroMeleeAttack(this, 2, 2.0f, meleeAttackRange);
        rangedAttack = new HeroRangedAttack(this, 1, 0.5f, 2.0f, bulletSpawner);
    }

    // Update is called once per frame
    protected override void Update()
    {
        if ((this as IHittable).dead())
        {
            // Do nothing here.
            // It is the Game's responsibility to react to this event.
            return;
        }

        base.Update();

        // 1. update shooting crossbar
        // 2. response to user inputs

        // 1.
        {
            var mouseScreenPos = Input.mousePosition;
            var mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
            shootingCrossbar.transform.position.Set(mouseWorldPos.x, mouseWorldPos.y, 0.0f);
        }

        // 2.
        {
            // Respond to direction keys
            var dirSum = Vector2.zero;
            if (Input.GetKeyDown(KeyCode.W)) dirSum += Vector2.up;
            if (Input.GetKeyDown(KeyCode.S)) dirSum += Vector2.down;
            if (Input.GetKeyDown(KeyCode.A)) dirSum += Vector2.left;
            if (Input.GetKeyDown(KeyCode.D)) dirSum += Vector2.right;
            direction = dirSum.normalized;

            // Respond to attack keys
            // LMB
            if(Input.GetMouseButtonDown(0))
            {
                rangedAttack.tryAttack(getPos());
            }
            // RMB
            if(Input.GetMouseButtonDown(1))
            {
                meleeAttack.tryAttack(getPos());
            }
        }

    }
}
