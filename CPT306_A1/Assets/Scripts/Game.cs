using System;
using UnityEngine;

/// <summary>
/// The only object that's put in the scene.
/// Handles everything else.
/// </summary>
public sealed class Game : MonoBehaviour
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

    // object prefabs
    GameObject obsPrefab;
    GameObject desObsPrefab;
    GameObject heroPrefab;
    GameObject meleeEnemyPrefab;
    GameObject rangedEnemyPrefab;

    // ui prefabs
    GameObject mainMenuPrefab;
    GameObject pauseMenuPrefab;
    GameObject nextLevelPrefab;
    GameObject victoryMenuPrefab;
    GameObject inGameMenuPrefab;

    // Will always be available before all's ctor
    // (because Game creates all, and in its ctor, the singleton var is assigned first).
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

        // Create the managers that don't need prefabs
        stateMgr = new StateManager();

        // Create the map
        map = new Map();

        // all that can't be inited here are inited in Awake().
    }

    /*********************************** Methods ***********************************/

    /// <summary>
    /// In MonoBehaviour.Start(), this is called to init all things
    /// and bring up the main UI
    /// </summary>
    public void onApplicationStart()
    {
        // init is done it ctor and Awake().

        uiMgr.showMainMenu();
    }

    /// <summary>
    /// called when the user starts the game from the main UI.
    /// - It reads the settings of `Level 1` from somewhere.
    ///     - and calls `resetLevel(1)` to create the level-dependent objects.
    ///     - calls `stateMgr.startGame()`
    /// </summary>
    public void startGame()
    {
        // stateMgr asserts the state already.

        // reset the level objects
        resetLevel(1);

        // state change
        stateMgr.startGame();
    }

    // called when the player wants to pause the game during gameplay
    public void pauseGame()
    {
        // stateMgr asserts the state already.
        
        stateMgr.pause();
        uiMgr.showPauseMenu();
    }

    public void resumeGame()
    {
        // stateMgr asserts the state already.

        stateMgr.resume();
        uiMgr.hideAllMenus();
    }

    /// <summary>
    /// called when the user continues to the next level from the level passed UI. 
    /// - It reads the settings of `Level 2` from somewhere.
    ///     - and calls `resetLevel(2)` to create the level-dependent objects.
    ///     - calls `stateMgr.continueGame()`
    /// </summary>
    public void continueGame()
    {        
        // stateMgr asserts the state already.

        // reset the level objects
        resetLevel(2);

        // state change
        stateMgr.continueGame();
    }

    /// <summary>
    /// When the player has won a level that is not the final one.
    /// Called by state mgr's timer
    /// </summary>
    public void nextLevel()
    {
        // stateMgr asserts the state already.

        stateMgr.nextLevel();

        uiMgr.showNextLevelMenu();
    }

    /// <summary>
    /// When the player has won the final level
    /// Called by state mgr's timer
    /// </summary>
    public void win()
    {
        // stateMgr asserts the state already.

        stateMgr.win();

        uiMgr.showVictoryMenu();
    }

    /// <summary>
    /// Called whenever the user wants to go back to main menu.
    /// </summary>
    public void goHome()
    {
        // stateMgr asserts the state already.

        // destroy all level objects
        map.clear();

        // state change
        stateMgr.goHome();

        // brings up the main ui
        uiMgr.showMainMenu();
    }

    /// <summary>
    /// Called when the user wants to exit the game
    /// </summary>
    public void exit()
    {
        // this time stateMgr can't assert anything
        // because I will not use it.
        Debug.Assert(stateMgr.getState() == StateManager.State.MAIN_UI);

        // destroy all level objects
        map.clear();

        Application.Quit(0);
    }

    /*********************************** Private Helpers ***********************************/
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
        for (int i = 0; i < numObsPerLevel[levelNum - 1]; ++i)
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

    /*********************************** MonoBehaviour ***********************************/
    /// <summary>
    /// Init managers and spawners
    /// </summary>
    private void Awake()
    {
        // Init what cannot be inited in the ctor
        // basically the things that need prefabs

        // ui mgr needs prefabs
        Debug.Assert(mainMenuPrefab != null, "Assign the prefab in the editor");
        Debug.Assert(pauseMenuPrefab != null, "Assign the prefab in the editor");
        Debug.Assert(nextLevelPrefab != null, "Assign the prefab in the editor");
        Debug.Assert(victoryMenuPrefab != null, "Assign the prefab in the editor");
        Debug.Assert(inGameMenuPrefab != null, "Assign the prefab in the editor");
        uiMgr = new UIManager(mainMenuPrefab, pauseMenuPrefab, nextLevelPrefab, victoryMenuPrefab, inGameMenuPrefab);

        // spawners need prefabs
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

    private void Start()
    {
        // everything before the main game menu.
        onApplicationStart();
    }

    /// <summary>
    /// All of the game logic is handled here.
    /// </summary>
    /// <exception cref="NotImplementedException">unfinished</exception>
    private void Update()
    {
        stateMgr.update(Time.deltaTime);

        // respond to key presses that bring up UI
        {
            // space pauses/resumes the game
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (stateMgr.getState() == StateManager.State.RUNNING)
                {
                    pauseGame();
                }
                else if (stateMgr.getState() == StateManager.State.PAUSED)
                {
                    resumeGame();
                }
            }
        }


        throw new NotImplementedException();
    }
}
