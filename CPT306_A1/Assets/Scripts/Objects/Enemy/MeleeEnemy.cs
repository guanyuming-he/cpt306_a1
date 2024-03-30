using System;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    /*********************************** Settings ***********************************/
    public static readonly Vector2 meleeAttackRange = new Vector2(3.0f, 3.0f);

    /*********************************** Fields ***********************************/
    // Assigned in editor
    public GameObject meleeVisualEffectPrefab;

    /*********************************** Ctor ***********************************/
    public MeleeEnemy() : base() 
    {
        // NOTE: unspecified
        speed = 0.5f;
    }

    /*********************************** Mono ***********************************/
    protected override void Awake()
    {
        base.Awake();

        // create the attack
        Game.MyDebugAssert(meleeVisualEffectPrefab != null);
        attack = new EnemyMeleeAttack(this, 1, 1.0f, meleeAttackRange, meleeVisualEffectPrefab);

        // create the hittable component
        hittableComp = gameObject.AddComponent<EnemyHittableComp>();
        // specified.
        hittableComp.initHealth(2);
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        if (!Game.gameSingleton.running())
        {
            return;
        }

        // Game logic:
        {
            var hittable = gameObject.GetComponent<IHittable>();
            if (hittable.dead())
            {
                Game.gameSingleton.spawnAnotherMelee();
            }
        }

        var heroPos = Game.gameSingleton.map.hero.getPos();
        var vectorDiff = heroPos - getPos();

        // chase the hero
        this.direction = vectorDiff.normalized;
        base.Update();

        // if it gets close enough to the hero, then attack.
        // by close enough, I want the attack range can cover at least 1.0f more than the hero.
        if (vectorDiff.magnitude <= meleeAttackRange.magnitude - 1.0f)
        {
            attack.tryAttack(getPos());
        }
    }
}

