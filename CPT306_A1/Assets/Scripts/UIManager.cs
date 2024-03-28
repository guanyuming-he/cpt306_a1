using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using UnityEditor.Events;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    /*********************************** Fields ***********************************/
    // Assigned in editor
    // ui prefabs
    public GameObject mainMenu;
    public GameObject pauseMenu;
    public GameObject nextLevelMenu;
    public GameObject victoryMenu;
    public GameObject gameOverMenu;
    public GameObject inGameMenu;
    public GameObject creditsMenu;
    public GameObject rankingsMenu;

    /*********************************** Ctor ***********************************/
    public UIManager() { }

    /*********************************** Methods ***********************************/
    /// <summary>
    /// Helper called inside the ctor
    /// </summary>
    private void bindButtonListeners()
    {
        // https://docs.unity3d.com/Manual/InspectorOptions.html#reordering-components
        // states that
        // The component order you apply in the Inspector is
        // the same order that you need to use when you query components in your scripts.

        // main menu buttons
        {
            var btns = mainMenu.GetComponentsInChildren<Button>();
            // start btn
            UnityEventTools.AddVoidPersistentListener(btns[0].onClick, onStartGameClicked);
            //btns[0].onClick.AddListener(onStartGameClicked);
            // ranking btn
            UnityEventTools.AddVoidPersistentListener(btns[1].onClick, onRankingClicked);
            // credits btn
            UnityEventTools.AddVoidPersistentListener(btns[2].onClick, onCreditsClicked);;
            // exit btn
            UnityEventTools.AddVoidPersistentListener(btns[3].onClick, onExitGameClicked);;
        }

        // pause menu buttons
        {
            var btns = pauseMenu.GetComponentsInChildren<Button>();
            // resume btn
            UnityEventTools.AddVoidPersistentListener(btns[0].onClick, onResumeGameClicked);;
            // restart btn
            UnityEventTools.AddVoidPersistentListener(btns[1].onClick, onRestartGameClicked);;
            // home btn
            UnityEventTools.AddVoidPersistentListener(btns[2].onClick, onHomeClicked);;
        }

        // next level buttons
        {
            var btns = nextLevelMenu.GetComponentsInChildren<Button>();
            // next level btn
            UnityEventTools.AddVoidPersistentListener(btns[0].onClick, onNextLevelClicked);;
            // restart btn
            UnityEventTools.AddVoidPersistentListener(btns[1].onClick, onRestartGameClicked);;
            // home btn
            UnityEventTools.AddVoidPersistentListener(btns[2].onClick, onHomeClicked);;
        }

        // victory buttons
        {
            var btns = victoryMenu.GetComponentsInChildren<Button>();
            // restart btn
            UnityEventTools.AddVoidPersistentListener(btns[0].onClick, onRestartGameClicked);;
            // home btn
            UnityEventTools.AddVoidPersistentListener(btns[1].onClick, onHomeClicked);;
        }

        // gameover buttons
        {
            var btns = gameOverMenu.GetComponentsInChildren<Button>();
            // restart btn
            UnityEventTools.AddVoidPersistentListener(btns[0].onClick, onRestartGameClicked);;
            // home btn
            UnityEventTools.AddVoidPersistentListener(btns[1].onClick, onHomeClicked);;
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

        //throw new System.NotImplementedException("TODO: credits menu");
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
        // TODO: clear previous bindings
        // and bind the in game informations
        //Game.MyDebugAssert(false, "Not implemented.");
    }
    public void showCreditsMenu()
    {
        creditsMenu.SetActive(true);
    }
    public void showRankingsMenu()
    {
        rankingsMenu.SetActive(true);
        // TODO: read rankings from the file and show them to the menu
        Game.MyDebugAssert(false, "Not implemented.");
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
        // inGameMenu.SetActive(false);
    }

    public void hideAllUI()
    {
        hideAllExceptInGameUI();
        inGameMenu.SetActive(false);
    }

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

        Game.MyDebugAssert(inGameMenu != null);
        Game.MyDebugAssert(mainMenu != null);
        Game.MyDebugAssert(pauseMenu != null);
        Game.MyDebugAssert(nextLevelMenu != null);
        Game.MyDebugAssert(victoryMenu != null);
        Game.MyDebugAssert(gameOverMenu != null);
        Game.MyDebugAssert(creditsMenu != null);
        Game.MyDebugAssert(rankingsMenu != null);

        // don't forget also to bind the listeners after spawning.
        bindButtonListeners();

        // hide all, and the in game menu
        // when I want to show one, call the corresponding method.
        hideAllUI();
    }

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

    /*********************************** OnClickHandler ***********************************/
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
