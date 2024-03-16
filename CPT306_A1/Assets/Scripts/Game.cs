using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Xml.Linq;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class Game : MonoBehaviour
{
    /*********************************** Fields ***********************************/
    StateManager stateMgr = null;
    UIManager uiMgr = null;

    // contains all the level objects
    Map map;

    // spawners
    ObstacleSpawner obsSpawner;
    DesObsSpawner desObsSpawner;
    HeroSpawner heroSpawner;
    MeleeEnemySpawner meleeSpawner;
    RangedEnemySpawner rangedSpawner;

    /*********************************** Methods ***********************************/
    /// <summary>
    /// Destroys and recreates all level objects with the configured spawners
    /// </summary>
    private void resetLevel(int levelNum)
    {
        // destory all level objects
        map.clear();

        // recreate level objects with the spawners
        // according to the levelNum

        // bind objects to the UI manager

        throw new NotImplementedException();
    }

    /// <summary>
    /// called when the user starts/restarts the game from the main UI.
    /// - It reads the settings of `Level 1` from somewhere.
    ///     - uses them to configure the `Spawner`s
    ///     - and calls `resetLevel(1)` to create the level-dependent objects.
    ///     - calls `stateMgr.startGame()`
    /// </summary>
    public void startGame()
    {
        // read the level settings and configure the spawners
        throw new NotImplementedException();

        // reset the level objects
        resetLevel(1);

        // state change
        stateMgr.startGame();
    }

    /// <summary>
    /// called when the user continues to the next level from the level passed UI. 
    /// - It reads the settings of `Level 2` from somewhere.
    ///     - uses them to configure the `Spawner`s
    ///     - and calls `resetLevel(2)` to create the level-dependent objects.
    ///     - calls `stateMgr.continueGame()`
    /// </summary>
    public void continueGame()
    {
        // read the level settings and configure the spawners
        throw new NotImplementedException();

        // reset the level objects
        resetLevel(2);

        // state change
        stateMgr.continueGame();
    }

    /*********************************** MonoBehaviour ***********************************/
    /// <summary>
    /// Init managers and spawners
    /// </summary>
    void Awake()
    {
        stateMgr = new StateManager();
        uiMgr = new UIManager();

        throw new NotImplementedException("Init the spawners.");
    }

    /// <summary>
    /// All of the game logic is handled here.
    /// </summary>
    /// <exception cref="NotImplementedException">unfinished</exception>
    void Update()
    {
        throw new NotImplementedException();
    }
}
