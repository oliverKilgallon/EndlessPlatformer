using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UI_OptionsMenu : MonoBehaviour {

    public AudioMixer mixer;

    public Dropdown resolutionDropdown;

    public Dropdown qualityDropdown;

    private Resolution[] resolutions;

	void Start ()
    {
        resolutions = Screen.resolutions;

        List<string> options = new List<string>();
        List<string> qualityLevels = new List<string>();

        resolutionDropdown.ClearOptions();
        qualityDropdown.ClearOptions();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
        }

        for (int i = 0; i < QualitySettings.names.Length; i++)
        {
            qualityLevels.Add(QualitySettings.names[i]);
        }

        qualityDropdown.AddOptions(qualityLevels);
        resolutionDropdown.AddOptions(options);
	}

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetMasterVolume(float volumeLevel)
    {
        mixer.SetFloat("Master", volumeLevel);
    }

    public void SetMusicVolume(float volumeLevel)
    {
        mixer.SetFloat("Music", volumeLevel);
    }

    public void SetSfxVolume(float volumeLevel)
    {
        mixer.SetFloat("Sfx", volumeLevel);
    }

    public void SetQualityLevel(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
