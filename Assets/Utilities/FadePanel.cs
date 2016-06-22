using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadePanel : MonoBehaviour {

	public float fadeDuration;

	private Image panelImage;
	private Color colorStart;
	private Color colorEnd;

	// Use this for initialization
	void Start () {
		panelImage = gameObject.GetComponent<Image>();
		panelImage.color = Color.black;
		
		colorStart = panelImage.color;
		colorStart.a = 1f;
		colorEnd = panelImage.color;
		colorEnd.a = 0f;
		
		StartCoroutine(FadeIn());
	}
	
	protected IEnumerator FadeIn() {
		for (float t = 0f; t < fadeDuration; t += Time.deltaTime) {
			panelImage.color = Color.Lerp(colorStart, colorEnd, t/fadeDuration);
			yield return null;
		}
		gameObject.SetActive(false);
	}
}
