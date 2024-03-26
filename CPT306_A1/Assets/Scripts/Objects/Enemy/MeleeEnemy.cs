using System;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    /*********************************** Settings ***********************************/
    public static readonly Vector2 meleeAttackRange = new Vector2(3.0f, 3.0f);

    /*********************************** Ctor ***********************************/
    public MeleeEnemy() : base() 
    {
        // NOTE: unspecified
        speed = 2.5f;

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
        var heroPos = Game.gameSingleton.map.hero.getPos();
        var vectorDiff = heroPos - getPos();

        // chase the hero
        this.direction = vectorDiff.normalized;

        // if it gets close enough to the hero, then attack.
        // by close enough, I want the attack range can cover at least 1.0f more than the hero.
        if(vectorDiff.magnitude <= meleeAttackRange.magnitude - 1.0f)
        {
            attack.tryAttack(getPos());
        }

        base.Update();
    }
}

