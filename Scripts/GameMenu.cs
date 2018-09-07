using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{  
    void OnEnable ()
    {
        //SettingsMenu = new GameObject.FindObjectOfType<SettingsMenu. >();
    }

    void Start ()
    {
        MainMenu = MainMenuFrame;
    }

    public GameObject GameMenuCanvas;
    GameObject settingsMenu;
    public GameObject MainMenuFrame;
    public static GameObject MainMenu;

    GameControl gameControl;

    public static bool GameMenuToggle = false;

    // Update is called once per frame
    void Update()
    {
        settingsMenu = GameControl.settingsMenuFrame;

        if (GameMenuToggle)
        {
            GameMenuCanvas.SetActive(true);
        }
        else
        {
            GameMenuCanvas.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !InventoryMenu.InventoryMenuToggle && !Dialog.dialogMode)
        {
            GameMenuToggle = !GameMenuToggle;
            GameControl.isPaused = !GameControl.isPaused;
        }

        if (GameControl.isPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            GameMenuCanvas.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void Resume()
    {
        GameControl.isPaused = false;
        GameMenuToggle = !GameMenuToggle;
    }

    public void Settings()
    {
        MainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void CloseSettings()
    {
        MainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void Exit()
    {
        GameControl.isPaused = false;
        GameMenuToggle = !GameMenuToggle;
        Destroy(FindObjectOfType<GameControl>().gameObject);
        SceneManager.LoadScene("Title Screen");
    }

}
