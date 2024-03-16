using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MovingObject, IHittable
{
    /*********************************** Fields ***********************************/
    private int _health;
    int IHittable.health
    {
        get { return _health; }
        set { _health = value; }
    }

    // Created in Awake where prefabs are available
    private MeleeAttack meleeAttack;
    private RangedAttack rangedAttack;

    // Assigned in the editor, instantiated in Start()
    // location updated to mouse in Update().
    public GameObject shootingCrossbar;

    /*********************************** Ctor ***********************************/
    /// <summary>
    /// Init the health.
    /// </summary>
    public Hero()
    {
        // specified in the specs
        this._health = 30;
    }

    /*********************************** MonoBehaviour ***********************************/
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if((this as IHittable).dead())
        {
            // stateMgr.gameOver();
            destroy();
            throw new NotImplementedException();
        }
    }
}
