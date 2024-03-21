using System;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    /*********************************** Settings ***********************************/
    public static readonly Vector2 meleeAttackRange = new Vector2(3.0f, 3.0f);

    /*********************************** Ctor ***********************************/
    public MeleeEnemy() : base() 
    {
        // create the attack
        attack = new EnemyMeleeAttack(this, 1, 1.0f, meleeAttackRange);
    }

    /*********************************** Mono ***********************************/
    protected override void Awake()
    {
        base.Awake();

        // create the hittable component
        hittableComp = gameObject.AddComponent<EnemyHittableComp>();
        // specified.
        hittableComp.setHealth(2);
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        attack.update(Time.deltaTime);

        throw new NotImplementedException("Chase the hero.");
    }
}

