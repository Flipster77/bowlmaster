using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MusicManager : MonoBehaviour {

    /// <summary>
    /// The music tracks that are used in the game.
    /// </summary>
	public AudioClip[] musicTracks;
    /// <summary>
    /// The music track indexes used by each of the levels.
    /// </summary>
    public int[] levelMusicIndexes;
    /// <summary>
    /// The sole music manager instance used by the game.
    /// </summary>
    public static MusicManager Instance {
        get {
            if (_instance == null) {
                _instance = GameObject.FindObjectOfType<MusicManager>();
            }

            return _instance;
        }
    }

    private static MusicManager _instance = null;
    private AudioSource music;

    /// <summary>
    /// Checks if this instance is the sole MusicManager instance.
    /// Destroys the instance if it is not.
    /// </summary>
    void Awake() {
        // First instance
        if (_instance == null) {
            _instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
            GameObject.DontDestroyOnLoad(gameObject);
        }
        // All other instances
        else if (_instance != this) {
            GameObject.Destroy(gameObject);
        }
    }
	
    /// <summary>
    /// Sets up the music manager and starts playing the level music.
    /// </summary>
    /// <param name="scene">The scene that was loaded.</param>
    /// <param name="mode">The mode of scene loading used.</param>
	void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        SetupMusic();
		PlayLevelMusic(scene.buildIndex);
	}
	
    /// <summary>
    /// Changes the music volume to the specified value.
    /// </summary>
    /// <param name="volume">The volume to set the music to.</param>
	public void ChangeVolume(float volume) {
		if (volume >= PlayerPrefsManager.MIN_MUSIC_VOLUME && volume <= PlayerPrefsManager.MAX_MUSIC_VOLUME) {
			music.volume = volume;
		} else {
			Debug.LogError("Attempted to set master volume to invalid value: " + volume);
		}
	}

    /// <summary>
    /// Changes the music track playing to the music track at the specified track index.
    /// </summary>
    /// <param name="volume">The index of the music track to play.</param>
    public void ChangeTrack(int track) {
        if (track >= 0 && track <= musicTracks.Length-1) {
            PlayMusicTrack(track);
        }
        else {
            Debug.LogError("Attempted to set game track index to invalid value: " + track);
        }
    }
	
    /// <summary>
    /// Stops the music if it is currently playing.
    /// </summary>
	public void StopMusic() {
		if (music.isPlaying) {
			music.Stop();
		}
	}

    /// <summary>
    /// Sets up the audio source, music volume and track index for the game scene.
    /// </summary>
    private void SetupMusic() {
        if (music == null) {
            music = GetComponent<AudioSource>();
        }
        music.volume = PlayerPrefsManager.GetMusicVolume();
        levelMusicIndexes[2] = PlayerPrefsManager.GetGameTrackIndex();
    }
	
    /// <summary>
    /// Plays the music for the specified level index.
    /// </summary>
    /// <param name="level">The level index to play music for.</param>
	private void PlayLevelMusic(int level) {
        int trackIndex = levelMusicIndexes[level];

        PlayMusicTrack(trackIndex);
	}

    /// <summary>
    /// Plays the music with the specified track index.
    /// </summary>
    /// <param name="trackIndex">The track index of the music to play.</param>
    private void PlayMusicTrack(int trackIndex) {
        AudioClip levelClip = musicTracks[trackIndex];

        if (levelClip != null) {
            // If the music to play is different than what is currently playing
            /// - Stop the music and set the new music clip
            if (levelClip != music.clip) {
                StopMusic();
                music.clip = levelClip;
                music.loop = true;
            }
            // If music is not currently playing, start the music
            if (!music.isPlaying) {
                music.Play();
            }
        } else {
            Debug.LogError("Failed to play music. No music found for track index " + trackIndex);
        }
    }
}
