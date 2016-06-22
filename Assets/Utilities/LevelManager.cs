using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public void LoadLevel(string name) {
		Debug.Log("Loading level " + name);
		SceneManager.LoadScene(name);
	}
	
	public void LoadLevel(int index) {
		Debug.Log("Loading level " + index);
        SceneManager.LoadScene(index);
	}
	
	private IEnumerator WaitThenLoadLevel(string levelName) {
		yield return new WaitForSeconds(4);
		LoadLevel(levelName);
	}
	
	private IEnumerator WaitThenLoadLevel(int index) {
		yield return new WaitForSeconds(4);
		LoadLevel(index);
	}
	
	public void LoadNextLevel() {
		LoadLevel(SceneManager.GetActiveScene().buildIndex+1);
	}
	
	public void LevelComplete() {
		StartCoroutine(WaitThenLoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
	}
	
	public void QuitRequest() {
		Debug.Log("Quit requested");
		Application.Quit();
	}
	
	public void NewGame() {
		LoadLevel(1);
	}
	
	public void LostGame() {
		LoadLevel("99b Lose Screen");
		//StartCoroutine(WaitThenLoadLevel("99b Lose Screen"));
	}
}
