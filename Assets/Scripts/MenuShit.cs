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
    public void MainFromLevelSelect()
    {
        ToggleDualUI(Menus.LevelSelect, Menus.Main);
    }
    public void MainFromPause()
    {
        ToggleDualUI(Menus.Pause, Menus.Main);
    }
    public void MainFromDeath()
    {
        ToggleDualUI(Menus.Death, Menus.Main);
    }
    public void MainFromVictory()
    {
        ToggleDualUI(Menus.Victory, Menus.Main);
    }
    public void ChangeScene(int SelectedScene)
    {
        SceneManager.LoadScene(SelectedScene);
    }
}
