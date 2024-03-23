using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    /*********************************** Fields ***********************************/
    // passed in through ctor
    GameObject mainMenuPrefab;
    GameObject pauseMenuPrefab;
    GameObject nextLevelPrefab;
    GameObject victoryMenuPrefab;
    GameObject inGameMenuPrefab;

    /*********************************** Ctor ***********************************/
    public UIManager
    (
        GameObject mainMenu, GameObject pauseMenu, GameObject nextLevelMenu, 
        GameObject victoryMenu, GameObject inGameMenuPrefab
    )
    {
        // should be available before all.
        Debug.Assert(Game.gameSingleton != null);

        this.mainMenuPrefab = mainMenu;
        this.pauseMenuPrefab = pauseMenu;
        this.nextLevelPrefab = nextLevelMenu;
        this.victoryMenuPrefab = victoryMenu;
        this.inGameMenuPrefab = inGameMenuPrefab;
    }

    /*********************************** Methods ***********************************/
    public void showMainMenu()
    {
        // Don't forget also to bind the listeners.
        throw new System.NotImplementedException();
    }

    public void showPauseMenu()
    {
        // Don't forget also to bind the listeners.
        throw new System.NotImplementedException();
    }
    public void showNextLevelMenu()
    {
        // Don't forget also to bind the listeners.
        throw new System.NotImplementedException();
    }

    public void showVictoryMenu()
    {
        // Don't forget also to bind the listeners.
        throw new System.NotImplementedException();
    }

    public void showInGameMenu()
    {
        // Don't forget also to bind the data to the UI
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// Hide all except the in game menu
    /// </summary>
    /// <exception cref="System.NotImplementedException"></exception>
    public void hideAllMenus()
    {
        throw new System.NotImplementedException();
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
}
