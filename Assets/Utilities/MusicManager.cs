using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MusicManager : MonoBehaviour {

	public AudioClip[] musicTracks;
    public int[] levelMusicIndexes;

    private static MusicManager instance = null;
    private AudioSource music;

    void Awake() {

        if (instance != null && instance != this) {
            GameObject.Destroy(gameObject);
        }
        else {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }

    void Start() {
        SetupMusic();
        PlayLevelMusic(SceneManager.GetActiveScene().buildIndex);
	}
	
	void OnLevelWasLoaded(int level) {
        SetupMusic();
		PlayLevelMusic(level);
	}
	
	public void ChangeVolume(float volume) {
		if (volume >= 0f && volume <= 1f) {
			music.volume = volume;
		} else {
			Debug.LogError("Attempted to set master volume to invalid value: " + volume);
		}
	}
	
	public void StopMusic() {
		if (music.isPlaying) {
			music.Stop();
		}
	}

    private void SetupMusic() {
        if (music == null) {
            music = GetComponent<AudioSource>();
        }
        music.volume = PlayerPrefsManager.GetMasterVolume();
        levelMusicIndexes[2] = PlayerPrefsManager.GetGameTrackIndex();
    }
	
	private void PlayLevelMusic(int level) {
        int trackIndex = levelMusicIndexes[level];

        AudioClip levelClip = musicTracks[trackIndex];
		
		if (instance == this && levelClip != null) {
			Debug.Log("Playing clip: " + levelClip.ToString());
		
			if (levelClip != music.clip) {
				StopMusic();
				music.clip = levelClip;
				music.loop = true;
			}
			
			if (!music.isPlaying) {
				music.Play();
			}
		}
	}
}
