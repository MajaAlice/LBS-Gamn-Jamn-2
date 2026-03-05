using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuShit : MonoBehaviour
{
    // Objects To NOT Delete -Lud
    public GameObject GameCanvas;
    // UI Objects -Lud
    public GameObject MainMenu;
    public GameObject SettingsMenu;
    public GameObject LevelSelectMenu;
    public GameObject DeathMenu;
    public GameObject VictoryMenu;
    public GameObject Pause;

    public enum Menus
    {
        NULL,
        Main,
        Settings,
        LevelSelect,
        Death,
        Victory,
        Pause
    }
    
    private void Start()
    {
        DontDestroyOnLoad(GameCanvas);
    }
    // Used In Making Sure The Correct 2 UIs Changed -Lid -Lud
    public void ToggleDualUI(Menus TurnOff, Menus TurnOn)
    {
        switch (TurnOff)
        {
            case Menus.NULL:
                break;
            case Menus.Main:
                MainMenu.SetActive(false);
                break;
            case Menus.Settings:
                SettingsMenu.SetActive(false);
                break;
            case Menus.LevelSelect:
                LevelSelectMenu.SetActive(false);
                break;
            case Menus.Death:
                DeathMenu.SetActive(false);
                break;
            case Menus.Victory:
                VictoryMenu.SetActive(false);
                break;
            case Menus.Pause:
                Pause.SetActive(false);
                break;
        }
        switch (TurnOn)
        {
            case Menus.NULL:
                break;
            case Menus.Main:
                MainMenu.SetActive(true);
                break;
            case Menus.Settings:
                SettingsMenu.SetActive(true);
                break;
            case Menus.LevelSelect:
                LevelSelectMenu.SetActive(true);
                break;
            case Menus.Death:
                DeathMenu.SetActive(true);
                break;
            case Menus.Victory:
                VictoryMenu.SetActive(true);
                break;
            case Menus.Pause:
                Pause.SetActive(true);
                break;
        }
    }
    public void Quit() // Quits The Game -Lud
    {
        Application.Quit();
    }
    #region Main
    public void SettingsFromMain() // -Lud
    {
        ToggleDualUI(Menus.Main, Menus.Settings);
    }
    public void MainFromSettings() // -Lud
    {
        ToggleDualUI(Menus.Settings, Menus.Main);
    }
    public void LevelSelectFromMain()
    {
        ToggleDualUI(Menus.Main, Menus.LevelSelect);
    }
    #endregion
    #region LevelSelect
    public void MainFromLevelSelect()
    {
        ToggleDualUI(Menus.LevelSelect, Menus.Main);
    }
    public void ChangeScene(int SelectedScene)
    {
        ToggleDualUI(Menus.LevelSelect, Menus.NULL);
        SceneManager.LoadScene(SelectedScene);
    }
    #endregion
    public void MainFromPause()
    {
        Destroy(GameCanvas);
        SceneManager.LoadScene(0);
    }
    public void MainFromDeath()
    {
        Destroy(GameCanvas);
        SceneManager.LoadScene(0);
    }
    public void MainFromVictory()
    {
        Destroy(GameCanvas);
        SceneManager.LoadScene(0);
    }
}
