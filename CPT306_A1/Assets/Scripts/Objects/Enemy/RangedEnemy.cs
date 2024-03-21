using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public sealed class RangedEnemy : Enemy
{
    // assigned in the editor
    public GameObject bulletPrefab;
    // created in Awake()
    private static ProjSpawner bulletSpawner = null;

    public RangedEnemy() : base() 
    {
        // NOTE: unspecified
        speed = 2.5f;
    }

    protected override void Awake()
    {
        if(bulletSpawner == null)
        {
            // assigned in the editor
            Debug.Assert(bulletPrefab != null);
            bulletSpawner = new ProjSpawner(bulletPrefab);
        }

        // create the attack
        this.attack = new EnemyRangedAttack(this, 1, 1.0f, 2.0f, bulletSpawner);

        // create the hittable comp
        hittableComp = gameObject.AddComponent<EnemyHittableComp>();
        // specified
        hittableComp.setHealth(1);

        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        attack.update(Time.deltaTime);

        var heroPos = Game.gameSingleton.map.hero.getPos();
        var vectorDiff = heroPos - getPos();

        // move away from the hero
        this.direction = - vectorDiff.normalized;

        // attack whenever it can
        attack.tryAttack(getPos());

        base.Update();
    }
}
