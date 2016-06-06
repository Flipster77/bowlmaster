using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

    private Pin[] pins;

	// Use this for initialization
	void Start () {
        pins = GameObject.FindObjectsOfType<Pin>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.T)) {

            foreach (Pin pin in pins) {
                print(pin.name + ": " + pin.IsStanding());
            }
        }
	}
}
