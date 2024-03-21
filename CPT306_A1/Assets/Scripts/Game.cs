using System;
using UnityEngine;

public class Game : MonoBehaviour
{
    /*********************************** Fields ***********************************/
    StateManager stateMgr;
    UIManager uiMgr;

    // contains all the level objects
    public Map map;

    // spawners
    ObstacleSpawner obsSpawner;
    DesObsSpawner desObsSpawner;
    HeroSpawner heroSpawner;
    MeleeEnemySpawner meleeSpawner;
    RangedEnemySpawner rangedSpawner;

    GameObject obsPrefab;
    GameObject desObsPrefab;
    GameObject heroPrefab;
    GameObject meleeEnemyPrefab;
    GameObject rangedEnemyPrefab;

    // Will always be available before all Start() and Update().
    // Game acts as the mediator. All objects talk to it.
    public static Game gameSingleton = null;

    /*********************************** Level Settings ***********************************/
    private static readonly int numLevels = 2;
    private static readonly int[] numObsPerLevel = new int[]
    {
        15, 15
    };
    private static readonly int[] numDesObsPerLevel = new int[]
    {
        15, 15
    };
    private static readonly int[] numMeleePerLevel = new int[]
    {
        10, 15
    };
    private static readonly int[] numRangedPerLevel = new int[]
    {
        0, 5
    };

    /*********************************** Ctor ***********************************/
    public Game()
    {
        // can only have one instance per game
        Debug.Assert(gameSingleton == null);
        gameSingleton = this;

        // Create the managers
        stateMgr = new StateManager();
        uiMgr = new UIManager();

        // Create the map
        map = new Map();

        // Spawners are created instead in Awake(),
        // because they need to refer to prefabs.
    }

    /*********************************** Methods ***********************************/
    /// <summary>
    /// Destroys and recreates all level objects with the configured spawners
    /// </summary>
    /// <param name="levelNum">number of the level</param>
    private void resetLevel(int levelNum)
    {
        if (levelNum > numLevels || levelNum <= 0)
        {
            throw new ArgumentException("levelNum is incorrect");
        }

        // destory all level objects
        map.clear();

        // recreate level objects with the spawners
        // according to the levelNum
        createObjects(levelNum); 

        // bind objects to the UI manager
        throw new NotImplementedException();
    }

    /// <summary>
    /// Helper used inside resetLevel()
    /// </summary>
    /// <param name="levelNum"></param>
    private void createObjects(int levelNum)
    { 
        // obstacles first
        for(int i = 0; i < numObsPerLevel[levelNum-1]; ++i)
        {
            map.addObstacle(obsSpawner.spawnRandom(map));
        }
        for (int i = 0; i < numDesObsPerLevel[levelNum - 1]; ++i)
        {
            map.addObstacle(desObsSpawner.spawnRandom(map));
        }
        
        // then hero
        map.addHero(heroSpawner.spawnRandom(map));

        // then enemies
        for (int i = 0; i < numMeleePerLevel[levelNum - 1]; ++i)
        {
            map.addEnemy(meleeSpawner.spawnRandom(map));
        }
        for (int i = 0; i < numRangedPerLevel[levelNum - 1]; ++i)
        {
            map.addEnemy(rangedSpawner.spawnRandom(map));
        }
    }

    /// <summary>
    /// called when the user starts/restarts the game from the main UI.
    /// - It reads the settings of `Level 1` from somewhere.
    ///     - and calls `resetLevel(1)` to create the level-dependent objects.
    ///     - calls `stateMgr.startGame()`
    /// </summary>
    public void startGame()
    {
        // reset the level objects
        resetLevel(1);

        // state change
        stateMgr.startGame();
    }

    /// <summary>
    /// called when the user continues to the next level from the level passed UI. 
    /// - It reads the settings of `Level 2` from somewhere.
    ///     - and calls `resetLevel(2)` to create the level-dependent objects.
    ///     - calls `stateMgr.continueGame()`
    /// </summary>
    public void continueGame()
    {
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
        Debug.Assert(obsPrefab != null, "Assign the prefab in the editor");
        Debug.Assert(desObsPrefab != null, "Assign the prefab in the editor");
        Debug.Assert(heroPrefab != null, "Assign the prefab in the editor");
        Debug.Assert(meleeEnemyPrefab != null, "Assign the prefab in the editor");
        Debug.Assert(rangedEnemyPrefab != null, "Assign the prefab in the editor");

        heroSpawner = new HeroSpawner(heroPrefab);
        obsSpawner = new ObstacleSpawner(obsPrefab);
        desObsSpawner = new DesObsSpawner(desObsPrefab);
        meleeSpawner = new MeleeEnemySpawner(meleeEnemyPrefab);
        rangedSpawner = new RangedEnemySpawner(rangedEnemyPrefab);
    }

    /// <summary>
    /// All of the game logic is handled here.
    /// </summary>
    /// <exception cref="NotImplementedException">unfinished</exception>
    void Update()
    {
        stateMgr.update(Time.deltaTime);

        // respond to ui keys

        throw new NotImplementedException();
    }
}
