using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour {

    /// <summary>
    /// Loads the level with the specified name.
    /// </summary>
    /// <param name="name">The name of the level to load.</param>
	public void LoadLevel(string name) {
		Debug.Log("Loading level " + name);
		SceneManager.LoadScene(name);
	}
	
    /// <summary>
    /// Loads the level with the specified build index.
    /// </summary>
    /// <param name="index">The build index of the level to load.</param>
	public void LoadLevel(int index) {
		Debug.Log("Loading level " + index);
        SceneManager.LoadScene(index);
	}
	
    /// <summary>
    /// Loads the level with the specified name after waiting a number of seconds.
    /// </summary>
    /// <param name="levelName">The name of the level to load.</param>
    /// <param name="seconds">The number of seconds to wait before loading the level.</param>
    /// <returns></returns>
	private IEnumerator WaitThenLoadLevel(string levelName, int seconds) {
		yield return new WaitForSeconds(seconds);
		LoadLevel(levelName);
	}

    /// <summary>
    /// Loads the level with the specified build index after waiting a number of seconds.
    /// </summary>
    /// <param name="index">The build index of the level to load.</param>
    /// <param name="seconds">The number of seconds to wait before loading the level.</param>
    /// <returns></returns>
    private IEnumerator WaitThenLoadLevel(int index, int seconds) {
		yield return new WaitForSeconds(4);
		LoadLevel(index);
	}
	
    /// <summary>
    /// Loads the next level as determined by the build index.
    /// </summary>
	public void LoadNextLevel() {
		LoadLevel(SceneManager.GetActiveScene().buildIndex+1);
	}

    /// <summary>
    /// Loads the next level as determined by the build index after waiting four seconds.
    /// </summary>
    public void LevelComplete() {
		StartCoroutine(WaitThenLoadLevel(SceneManager.GetActiveScene().buildIndex + 1, 4));
	}
	
    /// <summary>
    /// Exits the application.
    /// </summary>
	public void QuitRequest() {
		Debug.Log("Quit requested");
		Application.Quit();
	}
	
    /// <summary>
    /// Starts a new game.
    /// </summary>
	public void NewGame() {
		LoadLevel("02 Game");
	}
}
