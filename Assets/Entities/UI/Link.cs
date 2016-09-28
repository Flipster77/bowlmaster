using UnityEngine;
using System.Collections;

public class Link : MonoBehaviour {

    public string LinkAddress;

    /// <summary>
    /// Check that the link has an address.
    /// </summary>
    void Start() {
        if (string.IsNullOrEmpty(LinkAddress)) {
            Debug.LogError("Link has no specified address.");
        }
    }

    /// <summary>
    /// Opens the link in the default web browser.
    /// </summary>
	public void OpenLink() {
        Application.OpenURL(LinkAddress);
    }
}
