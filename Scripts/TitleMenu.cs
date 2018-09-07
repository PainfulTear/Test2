using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour {

    public GameObject MainMenu;
    public GameObject SettingsMainMenu;
    public GameObject SettingsVideoMenu;
    public GameObject SettingsControlsMenu;
    //public GameObject SettingsAudioMenu;
    public GameObject CharCreateMenu;
    

    public void Play () {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);      
        CharCreateMenu.SetActive(true);
        MainMenu.SetActive(false);
}

	public void Load () {

	}

    public void Settings ()
    {
        SettingsMainMenu.SetActive(true);
        MainMenu.SetActive(false);
    }

    public void SettingsVideo()
    {
        SettingsMainMenu.SetActive(true);
    }

    /*
    public void SettingsAudio()
    {
        SettingsMainMenu.SetActive(true);
    }*/

    public void SettingsControls()
    {
        SettingsMainMenu.SetActive(true);
    }

    public void BackFromCharCreate ()
    {
        CharCreateMenu.SetActive(false);
        MainMenu.SetActive(true);
    }
    
    public void Exit () {
        Application.Quit();
	}


}
