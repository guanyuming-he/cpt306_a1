using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager
{
    /*********************************** Fields ***********************************/
    // passed in through ctor
    readonly GameObject mainMenu;
    readonly GameObject pauseMenu;
    readonly GameObject nextLevelMenu;
    readonly GameObject victoryMenu;
    readonly GameObject gameOverMenu;
    readonly GameObject inGameMenu;

    /*********************************** Ctor ***********************************/
    public UIManager
    (
        GameObject mainMenu, GameObject pauseMenu, GameObject nextLevelMenu, 
        GameObject victoryMenu, GameObject gameOverMenu, GameObject inGameMenu
    )
    {
        // should be available before all.
        Debug.Assert(Game.gameSingleton != null);

        // spawn all menus.
        this.mainMenu =  GameObject.Instantiate(mainMenu);
        this.pauseMenu = GameObject.Instantiate(pauseMenu);
        this.nextLevelMenu = GameObject.Instantiate(nextLevelMenu);
        this.victoryMenu = GameObject.Instantiate(victoryMenu);
        this.gameOverMenu = GameObject.Instantiate(gameOverMenu);
        this.inGameMenu = GameObject.Instantiate(inGameMenu);

        // don't forget also to bind the listeners after spawning.
        bindButtonListeners();

        // hide all, and the in game menu
        // when I want to show one, call the corresponding method.
        hideAllUI();
    }

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
            var btns = mainMenu.GetComponents<Button>();
            // start btn
            btns[0].onClick.AddListener(onStartGameClicked);
            // ranking btn
            btns[1].onClick.AddListener(onRankingClicked);
            // credits btn
            btns[2].onClick.AddListener(onCreditsClicked);
            // exit btn
            btns[3].onClick.AddListener(onExitClicked);
        }

        // pause menu buttons
        {
            var btns = pauseMenu.GetComponents<Button>();
            // resume btn
            btns[0].onClick.AddListener(onResumeGameClicked);
            // restart btn
            btns[1].onClick.AddListener(onRestartGameClicked);
            // home btn
            btns[2].onClick.AddListener(onHomeClicked);
        }

        // ...
        throw new System.NotImplementedException();

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
        throw new System.NotImplementedException();
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

    /*********************************** OnClickHandler ***********************************/
    // main menu methods
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
    public void onExitClicked()
    {
        Game.gameSingleton.exit();
    }

    // pause menu methods
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
}
