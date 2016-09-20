using UnityEngine;
using System.Collections;

public class PinInSpace : MonoBehaviour {

    /// <summary>
    /// The speed at which the pin rotates.
    /// </summary>
    public float rotationSpeed;
    /// <summary>
    /// The direction of the pin's rotation.
    /// </summary>
    public Vector3 rotationDirection;

    private Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.AddTorque(rotationDirection * rotationSpeed);
    }
}
