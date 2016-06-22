using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsController : MonoBehaviour {

	public LevelManager levelManager;
	public Slider volumeSlider;
	public Slider difficultySlider;
	
	private MusicManager musicManager;

	// Use this for initialization
	void Start () {
		musicManager = GameObject.FindObjectOfType<MusicManager>();
		volumeSlider.value = PlayerPrefsManager.GetMasterVolume();
		difficultySlider.value = (float) PlayerPrefsManager.GetDifficulty();
	}
	
	// Update is called once per frame
	void Update () {
		musicManager.ChangeVolume(volumeSlider.value);
	}
	
	public void SaveAndExit() {
		PlayerPrefsManager.SetMasterVolume(volumeSlider.value);
		PlayerPrefsManager.SetDifficulty((int)difficultySlider.value);
		levelManager.LoadLevel(0);
	}
	
	public void SetDefaults() {
		volumeSlider.value = 0.8f;
		difficultySlider.value = 1f;
	}
}
