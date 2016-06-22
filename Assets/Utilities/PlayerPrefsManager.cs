using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerPrefsManager : MonoBehaviour {

	const string MASTER_VOLUME_KEY = "master_volume";
	const string DIFFICULTY_KEY = "difficulty";
	const string LEVEL_KEY = "level_unlocked_";

	public static void SetMasterVolume(float volume) {
		if (volume >= 0f && volume <= 1f) {
			PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
		} else {
			Debug.LogError("Attempted to set master volume to invalid value: " + volume);
		}
	}
	
	public static float GetMasterVolume() {
		return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
	}
	
	
	public static void UnlockLevel(int level) {
		if (level <= SceneManager.GetActiveScene().buildIndex - 1) {
			PlayerPrefs.SetInt(LEVEL_KEY + level.ToString(), 1);
		} else {
			Debug.LogError("Attempted to unlock invalid level: " + level);
		}
	}
	
	public static bool IsLevelUnlocked(int level) {
		int levelValue = PlayerPrefs.GetInt(LEVEL_KEY + level.ToString());
		
		if (level <= SceneManager.GetActiveScene().buildIndex - 1) {
			return (levelValue == 1);
		} else {
			Debug.LogError("Attempted to check invalid level: " + level);
			return false;
		}
	}
	
	public static void SetDifficulty(int difficulty) {
		if (difficulty >= 1 && difficulty <= 3) {
			PlayerPrefs.SetInt(DIFFICULTY_KEY, difficulty);
		} else {
			Debug.LogError("Attempted to set difficulty to invalid value: " + difficulty);
		}
	}
	
	public static int GetDifficulty() {
		return PlayerPrefs.GetInt(DIFFICULTY_KEY);
	}
}
