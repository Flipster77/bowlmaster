using UnityEngine;

public class PlayerPrefsManager {

    public const float DEFAULT_MUSIC_VOLUME = 0.6f;
    public const int DEFAULT_GAME_TRACK_INDEX = 1;
    public const float DEFAULT_X_SENSITIVITY = 0.25f;
    public const float DEFAULT_Y_SENSITIVITY = 1f;

    public const float MAX_MUSIC_VOLUME = 1f;
    public const float MIN_MUSIC_VOLUME = 0f;
    public const int MAX_GAME_TRACK_INDEX = 2;
    public const float MAX_X_SENSITIVITY = 1f;
    public const float MIN_X_SENSITIVITY = 0.1f;
    public const float MAX_Y_SENSITIVITY = 2f;
    public const float MIN_Y_SENSITIVITY = 0.5f;

    private const string MUSIC_VOLUME_KEY = "music_volume";
    private const string GAME_TRACK_INDEX_KEY = "game_track_index";
	private const string X_SENSITIVITY_KEY = "x_sensitivity";
    private const string Y_SENSITIVITY_KEY = "y_sensitivity";

    /// <summary>
    /// Sets the music volume in player preferences to the specified value.
    /// </summary>
    /// <param name="volume">The music volume to set.</param>
	public static void SetMusicVolume(float volume) {
		if (volume >= MIN_MUSIC_VOLUME && volume <= MAX_MUSIC_VOLUME) {
			PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, volume);
		} else {
			Debug.LogError("Attempted to set master volume to invalid value: " + volume);
		}
	}
	
    /// <summary>
    /// Retrieves the music volume set in the player preferences.
    /// </summary>
    /// <returns>The music volume value.</returns>
	public static float GetMusicVolume() {
		return PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, DEFAULT_MUSIC_VOLUME);
	}

    /// <summary>
    /// Sets the music track index for the game scene in player preferences to the specified value.
    /// </summary>
    /// <param name="track">The track index to set.</param>
    public static void SetGameTrackIndex(int track) {
        if (track >= 0 && track <= MAX_GAME_TRACK_INDEX) {
            PlayerPrefs.SetInt(GAME_TRACK_INDEX_KEY, track);
        }
        else {
            Debug.LogError("Attempted to set game track index to invalid value: " + track);
        }
    }

    /// <summary>
    /// Retrieves the music track index for the game scene set in the player preferences.
    /// </summary>
    /// <returns>The music track index of the game scene.</returns>
    public static int GetGameTrackIndex() {
        return PlayerPrefs.GetInt(GAME_TRACK_INDEX_KEY, DEFAULT_GAME_TRACK_INDEX);
    }

    /// <summary>
    /// Sets the x sensitivity in player preferences to the specified value.
    /// </summary>
    /// <param name="sensitivity">The x sensitivity to set.</param>
    public static void SetXSensitivity(float sensitivity) {
		if (sensitivity >= MIN_X_SENSITIVITY && sensitivity <= MAX_X_SENSITIVITY) {
			PlayerPrefs.SetFloat(X_SENSITIVITY_KEY, sensitivity);
		} else {
			Debug.LogError("Attempted to set x sensitivity to invalid value: " + sensitivity);
		}
	}

    /// <summary>
    /// Retrieves the x sensitivity set in the player preferences.
    /// </summary>
    /// <returns>The x sensitivity value.</returns>
    public static float GetXSensitivity() {
		return PlayerPrefs.GetFloat(X_SENSITIVITY_KEY, DEFAULT_X_SENSITIVITY);
	}

    /// <summary>
    /// Sets the y sensitivity in player preferences to the specified value.
    /// </summary>
    /// <param name="sensitivity">The y sensitivity to set.</param>
    public static void SetYSensitivity(float sensitivity) {
        if (sensitivity >= MIN_Y_SENSITIVITY && sensitivity <= MAX_Y_SENSITIVITY) {
            PlayerPrefs.SetFloat(Y_SENSITIVITY_KEY, sensitivity);
        }
        else {
            Debug.LogError("Attempted to set y sensitivity to invalid value: " + sensitivity);
        }
    }

    /// <summary>
    /// Retrieves the y sensitivity set in the player preferences.
    /// </summary>
    /// <returns>The y sensitivity value.</returns>
    public static float GetYSensitivity() {
        return PlayerPrefs.GetFloat(Y_SENSITIVITY_KEY, DEFAULT_Y_SENSITIVITY);
    }
}
