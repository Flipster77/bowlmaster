using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

    public Ball ball;

    private Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = this.transform.position - ball.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (this.transform.position.z <= 1800f || !ball.inPlay) {
            //Vector3 cameraPosition = ball.transform.position + offset;
            //cameraPosition.x = 0f;

            this.transform.position = ball.transform.position + offset;
        }
	}
}
