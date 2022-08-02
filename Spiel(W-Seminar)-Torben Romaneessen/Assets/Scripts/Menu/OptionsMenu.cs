using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField]
    private AudioMixer _audioMixer;
    [SerializeField]
    private TMPro.TMP_Dropdown _resolutionDropdown;
    [SerializeField]
    private Slider _slider;

    [SerializeField]
    private Resolution[] _resolutions;


    private void Awake()
    {
        _slider.value = PlayerPrefs.GetInt("sliderSavedNumber");
    }


    private void Start()
    {
        _resolutions = Screen.resolutions;
        _resolutionDropdown.ClearOptions();
        List<string> options = new();

        int currentResolutionIndex = 0;

        for(int i = 0; i < _resolutions.Length; i++)
        {
            string option = _resolutions[i].width + " x " + _resolutions[i].height;
            options.Add(option);

            if(_resolutions[i].width == Screen.width && _resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        _resolutionDropdown.AddOptions(options);
        _resolutionDropdown.value = currentResolutionIndex;
        _resolutionDropdown.RefreshShownValue();

        _audioMixer.SetFloat("volume", PlayerPrefs.GetInt("sliderSavedNumber"));
    }


    private void Update()
    {
        PlayerPrefs.SetInt("sliderSavedNumber", (int)_slider.value);
    }


    private void SetResolution(int resolutionIndex)
    {
        FindObjectOfType<AudioManager>().Play("ClickSound");
        Resolution resolution = _resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }


    private void SetVolume(float volume)
    {
        FindObjectOfType<AudioManager>().Play("ClickSound");
        _audioMixer.SetFloat("volume", volume);
    }


    private void SetQuality(int qualityIndex)
    {
        FindObjectOfType<AudioManager>().Play("ClickSound");
        QualitySettings.SetQualityLevel(qualityIndex);
    }


    private void SetFullscreen(bool isFullscreen)
    {
        FindObjectOfType<AudioManager>().Play("ClickSound");
        Screen.fullScreen = isFullscreen;
    }
}
