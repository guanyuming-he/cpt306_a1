using System.Collections;
using System.Collections.Generic;
using UnityEditor.Events;
using UnityEngine;
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
        //throw new System.NotImplementedException();
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
        System.Diagnostics.Debug.Assert(Game.gameSingleton != null);

        // spawn all menus.
        // inGameMenu before pauseMenu so that if both shown,
        // then pauseMenu overlapps the in game one.
        this.inGameMenu= GameObject.Instantiate(inGameMenu);
        this.mainMenu = GameObject.Instantiate(mainMenu);
        this.pauseMenu = GameObject.Instantiate(pauseMenu);
        this.nextLevelMenu = GameObject.Instantiate(nextLevelMenu);
        this.victoryMenu = GameObject.Instantiate(victoryMenu);
        this.gameOverMenu = GameObject.Instantiate(gameOverMenu);

        // don't forget also to bind the listeners after spawning.
        bindButtonListeners();

        // hide all, and the in game menu
        // when I want to show one, call the corresponding method.
        hideAllUI();
    }


    /*********************************** OnClickHandler ***********************************/
    public void onStartGameClicked()
    {
        Game.gameSingleton.startGame();
    }
    public void onRankingClicked()
    {
        throw new System.NotImplementedException();
    }
    public void onCreditsClicked()
    {
        throw new System.NotImplementedException();
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
        Game.gameSingleton.nextLevel();
    }
}
