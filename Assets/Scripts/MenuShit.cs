using NUnit.Framework;
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
    public GameObject PauseScreen;
    // Levels -Lud
    public List<GameObject> Levels;

    public void Quit() // Quits The Game -Lud
    {
        Application.Quit();
    }
    public void Main()
    {
        MainMenu.SetActive(true);
        SettingsMenu.SetActive(false);
        LevelSelectMenu.SetActive(false);
        PauseScreen.SetActive(false);
    }
    public void Settings()
    {
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(true);
    }
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
        PauseScreen.SetActive(false);
        LevelSelectMenu.SetActive(true);
    }
}
