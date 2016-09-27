using UnityEngine;
using System.Collections;

public class PlatformContentSwapper : MonoBehaviour {

    /// <summary>
    /// Mobile platform only content in the scene.
    /// </summary>
    public GameObject[] mobileContent;
    /// <summary>
    /// Standalone platform only content in the scene.
    /// </summary>
    public GameObject[] standaloneContent;

	// Use this for initialization
	void Awake () {
        RemoveContentForOtherPlatforms();
	}
	
    /// <summary>
    /// Removes the content for platforms other than the current platform.
    /// </summary>
    private void RemoveContentForOtherPlatforms() {
        #if UNITY_STANDALONE
            RemoveMobileContent();
        #else
            RemoveStandaloneContent();
        #endif
    }

    /// <summary>
    /// Removes the mobile platform only content from the scene.
    /// </summary>
	private void RemoveMobileContent() {
        foreach (GameObject content in mobileContent) {
            GameObject.Destroy(content);
        }
    }

    /// <summary>
    /// Removes the standalone platform only content from the scene.
    /// </summary>
    private void RemoveStandaloneContent() {
        foreach (GameObject content in standaloneContent) {
            GameObject.Destroy(content);
        }
    }
}
