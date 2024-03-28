using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;

/// <summary>
/// The only object that's put in the scene.
/// Handles everything else.
/// </summary>
public sealed class Game : MonoBehaviour
{
    /*********************************** Fields ***********************************/
    public readonly StateManager stateMgr;
    // assigned in editor
    public UIManager uiMgr;

    // contains all the level objects
    public readonly Map map;

    // spawners
    ObstacleSpawner obsSpawner;
    DesObsSpawner desObsSpawner;
    HeroSpawner heroSpawner;
    MeleeEnemySpawner meleeSpawner;
    RangedEnemySpawner rangedSpawner;

    // object prefabs
    public GameObject obsPrefab;
    public GameObject desObsPrefab;
    public GameObject heroPrefab;
    public GameObject meleeEnemyPrefab;
    public GameObject rangedEnemyPrefab;

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
        Game.MyDebugAssert(gameSingleton == null);
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

        stateMgr.startGame();
        uiMgr.hideAllUI();

        // reset and create the level objects
        resetLevel(1);
    }

    /// <summary>
    /// called when the player wants to pause the game during gameplay
    /// </summary>
    public void pauseGame()
    {
        // stateMgr asserts the state already.
        
        stateMgr.pause();
        uiMgr.showPauseMenu();
    }

    /// <summary>
    /// called when the player resumes a paused game
    /// </summary>
    public void resumeGame()
    {
        // stateMgr asserts the state already.

        stateMgr.resume();
        uiMgr.hideAllExceptInGameUI();
    }

    /// <summary>
    /// Restart from level 1
    /// </summary>
    public void restartGame()
    {
        // stateMgr asserts the state already.

        stateMgr.restart();
        uiMgr.hideAllUI();

        // reset and create the level objects
        resetLevel(1);
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

        stateMgr.continueGame();
        uiMgr.hideAllUI();

        // reset and create the level objects
        resetLevel(2);
    }

    /// <summary>
    /// When the player has won a level that is not the final one.
    /// Called by state mgr's timer
    /// </summary>
    public void nextLevel()
    {
        // stateMgr asserts the state already.

        uiMgr.hideAllUI();

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

        uiMgr.hideAllUI();

        stateMgr.win();
        uiMgr.showVictoryMenu();
    }

    /// <summary>
    /// When the hero dies
    /// </summary>
    public void gameOver()
    {
        // stateMgr asserts the state already.

        uiMgr.hideAllUI();

        stateMgr.gameOver();
        uiMgr.showGameOverMenu();
    }

    /// <summary>
    /// Called whenever the user wants to go back to main menu.
    /// </summary>
    public void goHome()
    {
        // stateMgr asserts the state already.

        // destroy all level objects
        map.clear();
        uiMgr.hideAllUI();

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
        Game.MyDebugAssert(stateMgr.getState() == StateManager.State.MAIN_UI);

        // destroy all level objects
        map.clear();
        uiMgr.hideAllUI();

        // destroy all other persistent game objects
        // including self
        GameObject.Destroy(uiMgr.gameObject);
        GameObject.Destroy(this.gameObject);

        // ExitProcess(0);
        Application.Quit(0);
    }

    /*********************************** Private Helpers ***********************************/
    /// <summary>
    /// Destroys and recreates all level objects with the configured spawners
    /// 
    /// In addition, shows the in game ui
    /// </summary>
    /// <param name="levelNum">number of the level</param>
    private void resetLevel(int levelNum)
    {
        Game.MyDebugAssert(!(levelNum > numLevels || levelNum <= 0), "levelNum is incorrect");

        // destory all level objects
        map.clear();

        // recreate level objects with the spawners
        // according to the levelNum
        createObjects(levelNum);

        // shows the in game menu
        uiMgr.showInGameMenu();
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
        uiMgr = GameObject.Instantiate(uiMgr);

        // spawners need prefabs
        Game.MyDebugAssert(obsPrefab != null, "Assign the prefab in the editor");
        Game.MyDebugAssert(desObsPrefab != null, "Assign the prefab in the editor");
        Game.MyDebugAssert(heroPrefab != null, "Assign the prefab in the editor");
        Game.MyDebugAssert(meleeEnemyPrefab != null, "Assign the prefab in the editor");
        Game.MyDebugAssert(rangedEnemyPrefab != null, "Assign the prefab in the editor");
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
    private void Update()
    {
        stateMgr.update(Time.deltaTime);

        // respond to other game logic if the game is running
        if(stateMgr.getState() == StateManager.State.RUNNING)
        {
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

            // if the hero is dead
            Game.MyDebugAssert(map.hero != null, "Hero will always be there when running, even when dead.");
            var hittable = map.hero.gameObject.GetComponent<HeroHittableComp>();
            if (hittable.dead())
            {
                gameOver();
            }

            // victory conditions are checked in the state manager.
        }

        //throw new NotImplementedException("Check if I have missed anything");
    }

    /*********************************** Static Helpers ***********************************/

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern void ExitProcess(UInt32 uExitCode);

    /// <summary>
    /// Don't know who the fucked decided that Unity should catch and ignore all assertions and exceptions.
    /// I won't allow that to happen.
    /// </summary>
    public static void MyDebugAssert(bool condition, String msg = "")
    {
        if(!condition)
        {
            Debugger.Break();
            Debugger.Log(0, "Assertion", "Debug Assertion Failed!\n" + msg);
            //ExitProcess(-1);
            Application.Quit(-1);
        }
    }
}
