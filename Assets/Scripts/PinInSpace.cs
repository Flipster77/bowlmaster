using UnityEngine;
using System.Collections;

public class PinInSpace : MonoBehaviour {

    public float rotationSpeed;
    public Vector3 rotationDirection;

    private Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.AddTorque(rotationDirection * rotationSpeed);
    }
	
	void FixedUpdate () {
        
    }
}
