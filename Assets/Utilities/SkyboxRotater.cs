using UnityEngine;
using System.Collections;

public class SkyboxRotater : MonoBehaviour {

    public float rotationSpeed;
    private float rotation;

    private Light directionalLight;

    void Start() {
        directionalLight = FindObjectOfType<Light>();
    }

	
	void Update () {
        rotation += rotationSpeed * Time.deltaTime;
        rotation %= 360f;
        Material skyboxMaterial = new Material(RenderSettings.skybox);
        skyboxMaterial.SetFloat("_Rotation", rotation);
        RenderSettings.skybox = skyboxMaterial;

        directionalLight.transform.Rotate(0f, -rotationSpeed * Time.deltaTime, 0f, Space.World);
    }
}
