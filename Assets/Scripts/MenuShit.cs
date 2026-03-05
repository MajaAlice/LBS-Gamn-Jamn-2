using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MenuShit : MonoBehaviour
{
    // Parent Objects -Lud
    public GameObject LevelHolder;
    // UI Objects -Lud
    public GameObject MainMenu;
    public GameObject SettingsMenu;
    public GameObject LevelSelectMenu;
    public GameObject DeathMenu;
    public GameObject VictoryMenu;
    public GameObject Pause;
    // Levels -Lud
    public List<GameObject> Levels;

    public enum Menus
    {
        NULL,
        Main,
        Settings,
        LevelSelect,
        Death,
        Win,
        Pause
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
            case Menus.Win:
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
            case Menus.Win:
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

    }
}
