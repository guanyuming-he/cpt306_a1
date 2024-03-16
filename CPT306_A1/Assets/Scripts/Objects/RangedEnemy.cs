using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public sealed class RangedEnemy : Enemy
{
    // Needs the hero's position to run and fire.
    // Assigned by the spawner.
    public Hero hero;

    // assigned in the editor
    public GameObject bulletPrefab;

    public RangedEnemy() 
        : base(1) 
    {
    }

    private void Awake()
    {
        // create the attack now that I have the prefab (assigned in the editor)
        Debug.Assert(bulletPrefab != null);

        var attack = new EnemyRangedAttack(this, 1, 1.0f, 2.0f);
        attack.projSpawner = new ProjSpawner(bulletPrefab);
        this.attack = attack;
    }

    protected override void Update()
    {
        base.Update();

        throw new NotImplementedException();
    }
}
