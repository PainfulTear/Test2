using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameControl : MonoBehaviour
{
    void Start ()
    {
        settingsMenuFrame = settingsMenu;
    }

    public static GameControl control;
    public static bool CombatMode = false;

    public static GameObject settingsMenuFrame;

    public GameObject settingsMenu;
    public GameObject SettingsVideoMenu;
    public GameObject SettingsControlsMenu;

    public GameObject MainMenu;

    public static bool destroy = false;

    public static int Endurance;
    public static int CombatSkill;
    public static int WillPower;
    public static int Agility;
    public static int Dexterity;
    public static int Cunning;
    public static int Intelligence;
    public static int Wisdom;
    public static int Talent;

    public static string Name;

    public static bool isPaused = false;

    void Update()
    {
        
        if (settingsMenu == null)
        {
            Debug.Log("error");
        }

        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByBuildIndex(0))
        {
            if (isPaused)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    void Awake()
    {
        
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }
    }
     
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savegame.sav");

        GameData data = new GameData();

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/savegame.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savegame.sav", FileMode.Open);
            GameData data = (GameData)bf.Deserialize(file);
            file.Close();
        }
        
    }

    public void SettingsClose()
    {
        settingsMenu.SetActive(false);
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            MainMenu.SetActive(true);
        }
        else
        {
            GameMenu.MainMenu.SetActive(true);
        }

        
    }

    public void Settings()
    {
        settingsMenu.SetActive(true);
        MainMenu.SetActive(false);
    }

    public void SettingsVideo()
    {
        SettingsVideoMenu.SetActive(true);
        SettingsControlsMenu.SetActive(false);
    }

    public void SettingsControls()
    {
        SettingsVideoMenu.SetActive(false);
        SettingsControlsMenu.SetActive(true);
    }

}

[Serializable]
class GameData
{

}
