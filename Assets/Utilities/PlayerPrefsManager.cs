using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerPrefsManager : MonoBehaviour {

	const string MASTER_VOLUME_KEY = "master_volume";
	const string X_SENSITIVITY_KEY = "x_sensitivity";
    const string Y_SENSITIVITY_KEY = "y_sensitivity";
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
	
	public static void SetXSensitivity(float sensitivity) {
		if (sensitivity >= 0.1f && sensitivity <= 1f) {
			PlayerPrefs.SetFloat(X_SENSITIVITY_KEY, sensitivity);
		} else {
			Debug.LogError("Attempted to set x sensitivity to invalid value: " + sensitivity);
		}
	}
	
	public static float GetXSensitivity() {
		return PlayerPrefs.GetFloat(X_SENSITIVITY_KEY);
	}

    public static void SetYSensitivity(float sensitivity) {
        if (sensitivity >= 0.5f && sensitivity <= 2.5f) {
            PlayerPrefs.SetFloat(Y_SENSITIVITY_KEY, sensitivity);
        }
        else {
            Debug.LogError("Attempted to set y sensitivity to invalid value: " + sensitivity);
        }
    }

    public static float GetYSensitivity() {
        return PlayerPrefs.GetFloat(Y_SENSITIVITY_KEY);
    }
}
