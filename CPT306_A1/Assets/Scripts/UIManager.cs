using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
#if UNITY_EDITOR
using UnityEditor.Events;
#endif
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Must be a MonoBehaviour so that 
///     1. Unity delegate binding (e.g. onClick) works
///     2. Unity data binding works
/// </summary>
public class UIManager : MonoBehaviour
{
    /*********************************** Fields ***********************************/
    // Assigned in editor
    // UI prefabs
    public GameObject mainMenu;
    public GameObject pauseMenu;
    public GameObject nextLevelMenu;
    public GameObject victoryMenu;
    public GameObject gameOverMenu;
    public GameObject inGameMenu;
    public GameObject creditsMenu;
    public GameObject rankingsMenu;
    public GameObject optionsMenu;
    // Trash Unity's Dropdown has stupid bugs.
    // Can't use that. Instead, create a menu myself.
    public GameObject difficultyMenu;

    // other elements that need to be stored (callbacks don't have parameters)
    private Slider[] optionsMenuSliders;

    /*********************************** UI data binding ***********************************/

    // See https://docs.unity3d.com/Manual/UIE-Binding.html

    public string scoreStrProp
    {
        get => String.Format("Score:{0}", Game.gameSingleton.stateMgr.getScore());
    }

    public string heroHealthStrProp
    {
        get
        {
            var hero = Game.gameSingleton.map.hero;
            if (hero == null)
            {
                return "Player:\n0";
            }

            var hittable = hero.gameObject.GetComponent<HittableComponent>();
            Game.MyDebugAssert(hittable != null);
            return String.Format("Player:\n{0}", hittable.getHealth());
        }
    }

    public string timeStrProp
    {
        get => String.Format("Time:{0}", 30.0f - Game.gameSingleton.stateMgr.getLevelTime());
    }

    public string meleeCdStrProp
    {
        get
        {
            var atk = Game.gameSingleton.map.hero.getMeleeAttack();
            bool ready = !atk.inCd();
            if(ready)
            {
                return "Melee:\nReady.";
            }
            else
            {
                var timer = atk.getCdTimer();
                float cdTime = timer.fireTime - timer.getTimeElapsed();
                return String.Format
                (
                    "Melee:\n{0}", cdTime
                );
            }
        }
    }
    public string rangedCdStrProp
    {
        get
        {
            var atk = Game.gameSingleton.map.hero.getRangedAttack();
            bool ready = !atk.inCd();
            if (ready)
            {
                return "Ranged:\nReady.";
            }
            else
            {
                var timer = atk.getCdTimer();
                float cdTime = timer.fireTime - timer.getTimeElapsed();
                return String.Format
                (
                    "Ranged:\n{0}", cdTime
                );
            }
        }
    }

    /*********************************** Ctor ***********************************/
    public UIManager() { }

    /*********************************** Methods ***********************************/
    /// <summary>
    /// Helper called inside the ctor
    /// to bind all callbacks to the menus
    /// </summary>
    private void bindMenuCallbacks()
    {
        // https://docs.unity3d.com/Manual/InspectorOptions.html#reordering-components
        // states that
        // The component order you apply in the Inspector is
        // the same order that you need to use when you query components in your scripts.

#if UNITY_EDITOR

        // main menu buttons
        {
            var btns = mainMenu.GetComponentsInChildren<Button>();

            Game.MyDebugAssert(btns.Length == 5);

            // start btn
            UnityEventTools.AddVoidPersistentListener(btns[0].onClick, onStartGameClicked);
            //btns[0].onClick.AddListener(onStartGameClicked);
            // ranking btn
            UnityEventTools.AddVoidPersistentListener(btns[1].onClick, onRankingClicked);
            // credits btn
            UnityEventTools.AddVoidPersistentListener(btns[2].onClick, onCreditsClicked);
            // options btn
            UnityEventTools.AddVoidPersistentListener(btns[3].onClick, onOptionsClicked);
            // exit btn
            UnityEventTools.AddVoidPersistentListener(btns[4].onClick, onExitGameClicked);
        }

        // pause menu buttons
        {
            var btns = pauseMenu.GetComponentsInChildren<Button>();
            // resume btn
            UnityEventTools.AddVoidPersistentListener(btns[0].onClick, onResumeGameClicked);
            // restart btn
            UnityEventTools.AddVoidPersistentListener(btns[1].onClick, onRestartGameClicked);
            // home btn
            UnityEventTools.AddVoidPersistentListener(btns[2].onClick, onHomeClicked);
        }

        // next level buttons
        {
            var btns = nextLevelMenu.GetComponentsInChildren<Button>();
            // next level btn
            UnityEventTools.AddVoidPersistentListener(btns[0].onClick, onNextLevelClicked);
            // restart btn
            UnityEventTools.AddVoidPersistentListener(btns[1].onClick, onRestartGameClicked);
            // home btn
            UnityEventTools.AddVoidPersistentListener(btns[2].onClick, onHomeClicked);
        }

        // victory buttons
        {
            var btns = victoryMenu.GetComponentsInChildren<Button>();
            // restart btn
            UnityEventTools.AddVoidPersistentListener(btns[0].onClick, onRestartGameClicked);
            // home btn
            UnityEventTools.AddVoidPersistentListener(btns[1].onClick, onHomeClicked);
        }

        // gameover buttons
        {
            var btns = gameOverMenu.GetComponentsInChildren<Button>();
            // restart btn
            UnityEventTools.AddVoidPersistentListener(btns[0].onClick, onRestartGameClicked);
            // home btn
            UnityEventTools.AddVoidPersistentListener(btns[1].onClick, onHomeClicked);
        }

        // rankings menu buttons
        {
            var btns = rankingsMenu.GetComponentsInChildren<Button>();
            // go back btn
            UnityEventTools.AddVoidPersistentListener(btns[0].onClick, () => rankingsMenu.SetActive(false));
        }

        // credits menu buttons
        {
            var btns = creditsMenu.GetComponentsInChildren<Button>();
            // go back btn
            UnityEventTools.AddVoidPersistentListener(btns[0].onClick, () => creditsMenu.SetActive(false));
        }

        // options menu sliders and button
        {
            optionsMenuSliders = optionsMenu.GetComponentsInChildren<Slider>();
            Game.MyDebugAssert(optionsMenuSliders.Length == 3);

            // master volume slider
            UnityEventTools.AddVoidPersistentListener
            (
                optionsMenuSliders[0].onValueChanged,
                () => AudioManager.masterVolume = optionsMenuSliders[0].value
            );
            // music volume slider
            UnityEventTools.AddVoidPersistentListener
            (
                optionsMenuSliders[1].onValueChanged,
                () => AudioManager.musicVolume = optionsMenuSliders[1].value
            );
            // effects volume slider
            UnityEventTools.AddVoidPersistentListener
            (
                optionsMenuSliders[2].onValueChanged,
                () => AudioManager.effectsVolume = optionsMenuSliders[2].value
            );

            var btns = optionsMenu.GetComponentsInChildren<Button>();
            Game.MyDebugAssert(btns.Length == 2);
            // difficulty change button
            UnityEventTools.AddVoidPersistentListener(btns[0].onClick, () => difficultyMenu.SetActive(true));
            // go back button
            UnityEventTools.AddVoidPersistentListener(btns[1].onClick, () => optionsMenu.SetActive(false));
        }

        // difficulty menu buttons.
        {
            var btns = difficultyMenu.GetComponentsInChildren<Button>();
            Game.MyDebugAssert(btns.Length == 3);
            // easy
            UnityEventTools.AddVoidPersistentListener(btns[0].onClick, () =>
            {
                Game.gameSingleton.stateMgr.difficulty = StateManager.Difficulty.EASY;
                difficultyMenu.SetActive(false);
                updateOptionsMenu();
            });
            // normal
            UnityEventTools.AddVoidPersistentListener(btns[1].onClick, () =>
            {
                Game.gameSingleton.stateMgr.difficulty = StateManager.Difficulty.NORMAL;
                difficultyMenu.SetActive(false);
                updateOptionsMenu();
            });
            // hard
            UnityEventTools.AddVoidPersistentListener(btns[2].onClick, () =>
            {
                Game.gameSingleton.stateMgr.difficulty = StateManager.Difficulty.HARD;
                difficultyMenu.SetActive(false);
                updateOptionsMenu();
            });
        }
#else
        // main menu buttons
        {
            var btns = mainMenu.GetComponentsInChildren<Button>();

            Game.MyDebugAssert(btns.Length == 5);

            // start btn
            btns[0].onClick.AddListener(onStartGameClicked);
            //btns[0].onClick.AddListener(onStartGameClicked);
            // ranking btn
            btns[1].onClick.AddListener(onRankingClicked);
            // credits btn
            btns[2].onClick.AddListener(onCreditsClicked);
            // options btn
            btns[3].onClick.AddListener(onOptionsClicked);
            // exit btn
            btns[4].onClick.AddListener(onExitGameClicked);
        }

        // pause menu buttons
        {
            var btns = pauseMenu.GetComponentsInChildren<Button>();
            // resume btn
            btns[0].onClick.AddListener(onResumeGameClicked);
            // restart btn
            btns[1].onClick.AddListener(onRestartGameClicked);
            // home btn
            btns[2].onClick.AddListener(onHomeClicked);
        }

        // next level buttons
        {
            var btns = nextLevelMenu.GetComponentsInChildren<Button>();
            // next level btn
            btns[0].onClick.AddListener(onNextLevelClicked);
            // restart btn
            btns[1].onClick.AddListener(onRestartGameClicked);
            // home btn
            btns[2].onClick.AddListener(onHomeClicked);
        }

        // victory buttons
        {
            var btns = victoryMenu.GetComponentsInChildren<Button>();
            // restart btn
            btns[0].onClick.AddListener(onRestartGameClicked);
            // home btn
            btns[1].onClick.AddListener(onHomeClicked);
        }

        // gameover buttons
        {
            var btns = gameOverMenu.GetComponentsInChildren<Button>();
            // restart btn
            btns[0].onClick.AddListener(onRestartGameClicked);
            // home btn
            btns[1].onClick.AddListener(onHomeClicked);
        }

        // rankings menu buttons
        {
            var btns = rankingsMenu.GetComponentsInChildren<Button>();
            // go back btn
            btns[0].onClick.AddListener(() => rankingsMenu.SetActive(false));
        }

        // credits menu buttons
        {
            var btns = creditsMenu.GetComponentsInChildren<Button>();
            // go back btn
            btns[0].onClick.AddListener(() => creditsMenu.SetActive(false));
        }

        // options menu sliders and button
        {
            optionsMenuSliders = optionsMenu.GetComponentsInChildren<Slider>();
            Game.MyDebugAssert(optionsMenuSliders.Length == 3);

            // master volume slider
            optionsMenuSliders[0].onValueChanged.AddListener
            (
                (value) => AudioManager.masterVolume = value
            );
            // music volume slider
            optionsMenuSliders[1].onValueChanged.AddListener
            (
                (value) => AudioManager.musicVolume = value
            );
            // effects volume slider
            optionsMenuSliders[2].onValueChanged.AddListener
            (
                (value) => AudioManager.effectsVolume = value
            );

            var btns = optionsMenu.GetComponentsInChildren<Button>();
            Game.MyDebugAssert(btns.Length == 2);
            // difficulty change button
            btns[0].onClick.AddListener(() => difficultyMenu.SetActive(true));
            // go back button
            btns[1].onClick.AddListener(() => optionsMenu.SetActive(false));
        }

        // difficulty menu buttons.
        {
            var btns = difficultyMenu.GetComponentsInChildren<Button>();
            Game.MyDebugAssert(btns.Length == 3);
            // easy
            btns[0].onClick.AddListener(() =>
            {
                Game.gameSingleton.stateMgr.difficulty = StateManager.Difficulty.EASY;
                difficultyMenu.SetActive(false);
                updateOptionsMenu();
            });
            // normal
            btns[1].onClick.AddListener(() =>
            {
                Game.gameSingleton.stateMgr.difficulty = StateManager.Difficulty.NORMAL;
                difficultyMenu.SetActive(false);
                updateOptionsMenu();
            });
            // hard
            btns[2].onClick.AddListener(() =>
            {
                Game.gameSingleton.stateMgr.difficulty = StateManager.Difficulty.HARD;
                difficultyMenu.SetActive(false);
                updateOptionsMenu();
            });
        }
#endif
    }

    public void showMainMenu()
    {
        mainMenu.SetActive(true);
    }
    public void showPauseMenu()
    {
        pauseMenu.SetActive(true);
    }
    public void showNextLevelMenu()
    {
        nextLevelMenu.SetActive(true);
    }
    public void showVictoryMenu()
    {
        victoryMenu.SetActive(true);
    }
    public void showGameOverMenu()
    {
        gameOverMenu.SetActive(true);
    }
    public void showInGameMenu()
    {
        inGameMenu.SetActive(true);
    }
    public void showCreditsMenu()
    {
        creditsMenu.SetActive(true);
    }
    public void showOptionsMenu()
    {
        optionsMenu.SetActive(true);

        updateOptionsMenu();
    }
    public void showRankingsMenu()
    {
        rankingsMenu.SetActive(true);

        // read the scores from the file and display them
        {        
            // list of (score, datetime)
            var scoresList = readScoresFromFile();
            // sort the list by score (ascending order by default)
            scoresList.Sort((p1, p2) => p1.Key.CompareTo(p2.Key));

            // because the list is sorted in ascending order,
            // read from the end
            // Note: It's TMP_Text instead of TextMeshPro
            var menuTextEntries = rankingsMenu.GetComponentsInChildren<TMP_Text>();
            // the first one is the title. the last one is the exit button box.
            // hence length-2
            for (int i = 0; i < menuTextEntries.Length - 2; ++i)
            {
                // the first one is the title. So i+1
                var textEntry = menuTextEntries[i + 1];
                // has a score for this one
                if (i < scoresList.Count)
                {
                    var scoresLine = scoresList[scoresList.Count - i - 1];
                    textEntry.text =
                    String.Format
                    (
                        "{0}: {1} at {2}",
                        i+1, scoresLine.Key, scoresLine.Value
                    );
                }
                // does not have a score for this one
                else
                {
                    textEntry.text = "No data";
                }
            }
        }
    }

    /// <summary>
    /// Hide all except the in game menu
    /// </summary>
    /// <exception cref="System.NotImplementedException"></exception>
    public void hideAllExceptInGameUI()
    {
        mainMenu.SetActive(false);
        pauseMenu.SetActive(false);
        nextLevelMenu.SetActive(false);
        victoryMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        creditsMenu.SetActive(false);
        rankingsMenu.SetActive(false);
        optionsMenu.SetActive(false);
        difficultyMenu.SetActive(false);
        // inGameMenu.SetActive(false);
    }

    public void hideAllUI()
    {
        hideAllExceptInGameUI();
        inGameMenu.SetActive(false);
    }

    public void OnDestroy()
    {
        GameObject.Destroy(mainMenu.gameObject);
        GameObject.Destroy(pauseMenu.gameObject);
        GameObject.Destroy(nextLevelMenu.gameObject);
        GameObject.Destroy(victoryMenu.gameObject);
        GameObject.Destroy(gameOverMenu.gameObject);
        GameObject.Destroy(creditsMenu.gameObject);
        GameObject.Destroy(rankingsMenu.gameObject);
        GameObject.Destroy(optionsMenu.gameObject);
        GameObject.Destroy(difficultyMenu.gameObject);
        GameObject.Destroy(inGameMenu.gameObject);
    }

    /*********************************** Mono ***********************************/

    private void Awake()
    {
        // should be available before all.
        Game.MyDebugAssert(Game.gameSingleton != null);

        // spawn all menus.
        // inGameMenu before pauseMenu so that if both shown,
        // then pauseMenu overlapps the in game one.
        this.inGameMenu= GameObject.Instantiate(inGameMenu);
        this.mainMenu = GameObject.Instantiate(mainMenu);
        this.pauseMenu = GameObject.Instantiate(pauseMenu);
        this.nextLevelMenu = GameObject.Instantiate(nextLevelMenu);
        this.victoryMenu = GameObject.Instantiate(victoryMenu);
        this.gameOverMenu = GameObject.Instantiate(gameOverMenu);
        this.creditsMenu = GameObject.Instantiate(creditsMenu);
        this.rankingsMenu = GameObject.Instantiate(rankingsMenu);
        this.optionsMenu = GameObject.Instantiate(optionsMenu);
        this.difficultyMenu = GameObject.Instantiate(difficultyMenu);

        Game.MyDebugAssert(inGameMenu != null);
        Game.MyDebugAssert(mainMenu != null);
        Game.MyDebugAssert(pauseMenu != null);
        Game.MyDebugAssert(nextLevelMenu != null);
        Game.MyDebugAssert(victoryMenu != null);
        Game.MyDebugAssert(gameOverMenu != null);
        Game.MyDebugAssert(creditsMenu != null);
        Game.MyDebugAssert(rankingsMenu != null);
        Game.MyDebugAssert(optionsMenu != null);
        Game.MyDebugAssert(difficultyMenu != null);

        // don't forget also to bind the listeners after spawning.
        bindMenuCallbacks();

        // hide all, and the in game menu
        // when I want to show one, call the corresponding method.
        hideAllUI();
    }

    private void Update()
    {
        // when the game is running
        if(Game.gameSingleton.stateMgr.getState() == StateManager.State.RUNNING)
        {
            // update the in game menu's texts
            if(inGameMenu.activeSelf)
            {
                var texts = inGameMenu.GetComponentsInChildren<TMP_Text>();
                Game.MyDebugAssert(texts.Length == 5);

                // 0. time text
                texts[0].text = timeStrProp;
                // 1. score text
                texts[1].text = scoreStrProp;
                // 2. player health text
                texts[2].text = heroHealthStrProp;
                // 3. melee cd text
                texts[3].text = meleeCdStrProp;
                // 4. ranged cd text
                texts[4].text = rangedCdStrProp;
            }
        }
    }

    /*********************************** Helpers ***********************************/

    /// <returns>A list of scores stored in the scores file, in the order they are stored in the file. 
    /// or an empty one if the file does not exit</returns>
    private List<KeyValuePair<int, DateTime>> readScoresFromFile()
    {
        List<KeyValuePair<int, DateTime>> ret = new List<KeyValuePair<int, DateTime>>();

        if(File.Exists(StateManager.SCORE_FILE_PATH))
        {
            // Open the file to read from.
            using (StreamReader sr = File.OpenText(StateManager.SCORE_FILE_PATH))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    // get the score and datetime strings separated by ...
                    int separatorInd = s.IndexOf("...");
                    Game.MyDebugAssert(separatorInd != -1, "The score file is corrupted.");
                    string scoreStr = s.Substring(0, separatorInd);
                    string dateTimeStr = s.Substring(separatorInd + 3);

                    // parse the strings
                    int score = -1;
                    DateTime dt = DateTime.Now;
                    try
                    {
                        score = Int32.Parse(scoreStr);
                        dt = DateTime.Parse(dateTimeStr);
                        
                    }
                    catch(ArgumentException)
                    {
                        Game.MyDebugAssert(false, "The score file is corrupted.");
                    }

                    // add the result to the list.
                    var scoreLine = new KeyValuePair<int, DateTime>(score, dt);
                    ret.Add(scoreLine);
                }
            }
        }

        return ret;
    }

    private void updateOptionsMenu()
    {
        // Set difficulty text
        var btns = optionsMenu.GetComponentsInChildren<Button>();
        btns[0].GetComponentInChildren<TMP_Text>().text =
            "Current Difficulty: " +
            Game.gameSingleton.stateMgr.difficulty.ToString() +
            "\nClick to CHANGE";
    }

    /*********************************** UI Callbacks ***********************************/
    public void onStartGameClicked()
    {
        Game.gameSingleton.startGame();
    }
    public void onRankingClicked()
    {
        showRankingsMenu();
    }
    public void onCreditsClicked()
    {
        showCreditsMenu();
    }
    public void onOptionsClicked()
    {
        showOptionsMenu();
    }
    public void onExitGameClicked()
    {
        Game.gameSingleton.exit();
    }
    public void onResumeGameClicked()
    {
        Game.gameSingleton.resumeGame();
    }
    public void onRestartGameClicked()
    {
        Game.gameSingleton.restartGame();
    }
    public void onHomeClicked()
    {
        Game.gameSingleton.goHome();
    }
    public void onNextLevelClicked()
    {
        Game.gameSingleton.continueGame();
    }
}
