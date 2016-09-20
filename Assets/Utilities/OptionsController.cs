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
        musicManager = MusicManager.Instance;
        if (musicDropdown != null) {
            musicDropdown.value = PlayerPrefsManager.GetGameTrackIndex();
        }
        
        volumeSlider.value = PlayerPrefsManager.GetMusicVolume();
		xSensitivitySlider.value = PlayerPrefsManager.GetXSensitivity();
        ySensitivitySlider.value = PlayerPrefsManager.GetYSensitivity();
    }

    /// <summary>
    /// Changes the volume of the music manager.
    /// </summary>
    /// <param name="volume">The volume to set the music manager.</param>
    public void ChangeVolume(float volume) {
        if (musicManager != null) {
            musicManager.ChangeVolume(volume);
        }
    }

    /// <summary>
    /// Changes the music track for the game scene in the music manager.
    /// </summary>
    /// <param name="track">The index of the track to set.</param>
    public void ChangeTrack(int track) {
        if (musicManager != null) {
            musicManager.ChangeTrack(track);
        }
    }

    /// <summary>
    /// Saves the current options values.
    /// </summary>
    public void SaveSettings() {
        PlayerPrefsManager.SetGameTrackIndex(musicDropdown.value);
        PlayerPrefsManager.SetMusicVolume(volumeSlider.value);
        PlayerPrefsManager.SetXSensitivity(xSensitivitySlider.value);
        PlayerPrefsManager.SetYSensitivity(ySensitivitySlider.value);
    }

    /// <summary>
    /// Saves the current options values and loads the main menu.
    /// </summary>
	public void SaveAndExit() {
        SaveSettings();
        levelManager.LoadLevel(0);
	}
	
    /// <summary>
    /// Reverts the displayed options to their default values.
    /// </summary>
	public void RevertToDefaults() {
        musicDropdown.value = PlayerPrefsManager.DEFAULT_GAME_TRACK_INDEX;
		volumeSlider.value = PlayerPrefsManager.DEFAULT_MUSIC_VOLUME;
        xSensitivitySlider.value = PlayerPrefsManager.DEFAULT_X_SENSITIVITY;
        ySensitivitySlider.value = PlayerPrefsManager.DEFAULT_Y_SENSITIVITY;
	}
}
