using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsController : MonoBehaviour {

	public LevelManager levelManager;
    public Dropdown musicDropdown;
	public Slider volumeSlider;
	public Slider xSensitivitySlider;
    public Slider ySensitivitySlider;

    private MusicManager musicManager;

	// Use this for initialization
	void Start () {
		musicManager = GameObject.FindObjectOfType<MusicManager>();
        if (musicDropdown != null) {
            musicDropdown.value = PlayerPrefsManager.GetGameTrackIndex();
        }
        
        volumeSlider.value = PlayerPrefsManager.GetMasterVolume();
		xSensitivitySlider.value = PlayerPrefsManager.GetXSensitivity();
        ySensitivitySlider.value = PlayerPrefsManager.GetYSensitivity();
    }
	
	// Update is called once per frame
	void Update () {
        if (musicManager != null) {
            musicManager.ChangeVolume(volumeSlider.value);
        }
	}

    public void SaveSettings() {
        PlayerPrefsManager.SetGameTrackIndex(musicDropdown.value);
        PlayerPrefsManager.SetMasterVolume(volumeSlider.value);
        PlayerPrefsManager.SetXSensitivity(xSensitivitySlider.value);
        PlayerPrefsManager.SetYSensitivity(ySensitivitySlider.value);
    }

	public void SaveAndExit() {
        SaveSettings();
        levelManager.LoadLevel(0);
	}
	
	public void RevertToDefaults() {
        musicDropdown.value = PlayerPrefsManager.DEFAULT_GAME_TRACK_INDEX;
		volumeSlider.value = PlayerPrefsManager.DEFAULT_VOLUME;
        xSensitivitySlider.value = PlayerPrefsManager.DEFAULT_X_SENSITIVITY;
        ySensitivitySlider.value = PlayerPrefsManager.DEFAULT_Y_SENSITIVITY;
	}
}
