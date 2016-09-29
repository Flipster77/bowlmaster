using UnityEngine;
using System.Collections;

public class SkyboxRotater : MonoBehaviour {

    /// <summary>
    /// The number of degrees per second that the skybox is rotated by.
    /// </summary>
    public float rotationSpeed;
    /// <summary>
    /// Whether or not the skybox rotation is currently enabled.
    /// </summary>
    public bool rotationEnabled;
    /// <summary>
    /// The current rotation value of the skybox.
    /// </summary>
    private float rotation;

    private Light directionalLight;

    void Awake() {
        directionalLight = FindObjectOfType<Light>();
        #if UNITY_STANDALONE
            rotationEnabled = true;
        #else
            rotationEnabled = false;
        #endif
    }

	/// <summary>
    /// Rotates the skybox and the directional light each frame.
    /// </summary>
	void Update () {
        if (rotationEnabled) {
            rotation += rotationSpeed * Time.deltaTime;
            rotation %= 360f;
            Material skyboxMaterial = new Material(RenderSettings.skybox);
            skyboxMaterial.SetFloat("_Rotation", rotation);
            RenderSettings.skybox = skyboxMaterial;

            directionalLight.transform.Rotate(0f, -rotationSpeed * Time.deltaTime, 0f, Space.World); 
        }
    }
}
