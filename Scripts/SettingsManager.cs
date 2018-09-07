using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public Toggle fullscreenToggle;
    public Dropdown resolutionDropdown;
    public Dropdown textureQuialityDropdown;
    public Dropdown antialiasingDropdown;
    public Dropdown vSynkDropdown;
    public Slider musicVolumeSlider;
    public Button applyButton;

    public Resolution[] resolutions;
    public GameSettings gameSettings;
    public AudioSource musicSource;

    void OnEnable()
    {
        gameSettings = new GameSettings();

        LoadSettings();

        fullscreenToggle.onValueChanged.AddListener(delegate { OnFullscreenToggle(); });
        resolutionDropdown.onValueChanged.AddListener(delegate { OnResoulutionChange(); });
        textureQuialityDropdown.onValueChanged.AddListener(delegate { OnTextureQualityChange(); });
        antialiasingDropdown.onValueChanged.AddListener(delegate { OnAntialiasingChange(); });
        vSynkDropdown.onValueChanged.AddListener(delegate { OnAntialiasingChange(); });
        musicVolumeSlider.onValueChanged.AddListener(delegate { OnMusicVolumeChange(); });
        applyButton.onClick.AddListener(delegate { OnApplyButtonClick(); });

        resolutions = Screen.resolutions;
        foreach (Resolution resolution in resolutions)
        {
            resolutionDropdown.options.Add(new Dropdown.OptionData(resolution.ToString()));
        }  
    }

    public void OnFullscreenToggle()
    {
        gameSettings.isFullscreen = Screen.fullScreen = fullscreenToggle.isOn;
    }

    public void OnResoulutionChange()
    {
        Screen.SetResolution(resolutions[resolutionDropdown.value].width, resolutions[resolutionDropdown.value].height, Screen.fullScreen);
        gameSettings.resolutionIndex = resolutionDropdown.value;
    }

    public void OnTextureQualityChange()
    {
        QualitySettings.masterTextureLimit = textureQuialityDropdown.value;
        gameSettings.textureQuality = textureQuialityDropdown.value;
    }

    public void OnAntialiasingChange()
    {
        QualitySettings.antiAliasing = (int)Mathf.Pow(2f, antialiasingDropdown.value);
        gameSettings.antiAliasing = (int)Mathf.Pow(2f, antialiasingDropdown.value);
    }

    public void OnVSynkChange()
    {
        QualitySettings.vSyncCount = vSynkDropdown.value;
        gameSettings.vSync = vSynkDropdown.value;
    }

    public void OnMusicVolumeChange()
    {
        musicSource.volume = musicVolumeSlider.value;
        gameSettings.musicVolume = musicVolumeSlider.value;
    }

    public void OnApplyButtonClick()
    {
        SaveSettings();
    }

    public void SaveSettings()
    {
        string jsonData = JsonUtility.ToJson(gameSettings, true);
        File.WriteAllText(Application.persistentDataPath + "/settings.json", jsonData);
    }

    public void LoadSettings()
    {
        // If file is avaliable (TODO)
        gameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.persistentDataPath + "/settings.json"));
        musicVolumeSlider.value = gameSettings.musicVolume;
        antialiasingDropdown.value = gameSettings.antiAliasing;
        vSynkDropdown.value = gameSettings.vSync;
        textureQuialityDropdown.value = gameSettings.textureQuality;
        resolutionDropdown.value = gameSettings.resolutionIndex;
        fullscreenToggle.isOn = gameSettings.isFullscreen;
    }

}
