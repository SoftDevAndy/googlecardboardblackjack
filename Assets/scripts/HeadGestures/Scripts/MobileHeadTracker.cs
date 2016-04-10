/* MobileHeadTracker.cs
 * (c) 2014 Quantum Leap Computing (QLC)
 * Author: Dave Arendash
 */
 using UnityEngine;
using System.Collections;

// Very simple mobile tracker based on accelerometer/compass (which should work with older phones which have no gyros)
public class MobileHeadTracker : MonoBehaviour {

	public GUIText gText;
	bool gotInitHeading;
	float initHeading;
	// Use this for initialization
	void Start () {
		Input.compass.enabled = true;
		//Input.com
		Input.location.Start();
		gotInitHeading = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!gotInitHeading)
		{
			initHeading = Input.compass.magneticHeading;
			gotInitHeading = true;
		}
		transform.eulerAngles =  new Vector3 (Input.acceleration.z * -90f, Input.compass.magneticHeading-initHeading, 0f);
		gText.text = Input.compass.magneticHeading.ToString();
	}
}
