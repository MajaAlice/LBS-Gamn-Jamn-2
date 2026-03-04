using System.Collections.Generic;
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
    void ToggleDualUI(Menus TurnOff, Menus TurnOn)
    {
        switch(TurnOff)
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
    }

    public void Quit() // Quits The Game -Lud
    {
        Application.Quit();
    }
    public void Main()
    {
        MainMenu.SetActive(true);
        SettingsMenu.SetActive(false);
        LevelSelectMenu.SetActive(false);
        DeathMenu.SetActive(false);
    }
    // Goes From Menu To Settings -Lud
    public void Settings()
    {
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(true);
    }
    // Loads The Level On The Holder And Turns Of UI .-.
    public void SelectLevel(int SelectedLevel)
    {
        LevelSelectMenu.SetActive(false);
        Instantiate(Levels[SelectedLevel], LevelHolder.transform);
    }
    public void LevelSelectFromMain()
    {
        MainMenu.SetActive(false);
        LevelSelectMenu.SetActive(true);
    }
    public void LevelSelectFromPauseScreen()
    {
        DeathMenu.SetActive(false);
        LevelSelectMenu.SetActive(true);
    }
}
