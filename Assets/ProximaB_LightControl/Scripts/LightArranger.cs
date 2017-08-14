// script attached to the gameobject Artnet_Ctrl / Geometries_Debug

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightArranger : MonoBehaviour {
	float default_brightness;
	public FadingShapes fadingShapes;
	public DefaultBrightness defaultBrightness;

	void Start () {
		default_brightness = defaultBrightness.val;
		print("default_brightness: " + default_brightness);

		
		for(int i = 0; i < transform.childCount; i++){
			Transform currentLight = transform.GetChild(i);
			currentLight.gameObject.GetComponent<Renderer>().material.color = new Color(default_brightness, default_brightness, default_brightness, 1);
			currentLight.position = new Vector3(currentLight.position.x + i, currentLight.position.y, currentLight.position.z);
		}
		print(transform.childCount + " lights available.");
	}
}
