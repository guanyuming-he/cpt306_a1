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

    public RangedEnemy() : base() { }

    protected override void Awake()
    {
        base.Awake();

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
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        attack.update(Time.deltaTime);

        throw new NotImplementedException("Move away from the hero");
    }
}
