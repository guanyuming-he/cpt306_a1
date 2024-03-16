using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class Game : MonoBehaviour
{
    /*********************************** Fields ***********************************/
    StateManager stateMgr = null;
    UIManager uiMgr = null;

    // contains all the level objects
    Map map;

    // used to respawn enemies as some levels require that.
    EnemySpawner enemySpawner = null;

    /*********************************** Methods ***********************************/
    /// <summary>
    /// Destroys and recreates all level objects
    /// </summary>
    private void resetLevel(ObstacleSpawner obstacleSpawner, HeroSpawner heroSpawner, EnemySpawner enemySpawner)
    {
        // destory all level objects
        map.clear();

        // recreate level objects with the spawners

        // bind objects to the UI manager

        throw new NotImplementedException();
    }

    /*********************************** MonoBehaviour ***********************************/
    // Start is called before the first frame update
    void Start()
    {
        stateMgr = new StateManager();
        uiMgr = new UIManager();
    }

    // Update is called once per frame
    void Update()
    {
        throw new NotImplementedException();
    }
}
