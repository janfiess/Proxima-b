/*
	This script is attached to the Artnet_Ctrl game object which is always available in the scene.
	This script reads the color values of Unity software lights in realtime (Update function) 
	and translates the RGB values to DMX devices
	The Unity software lights' colors are driven by the Animator
 */


using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using ArtNet;
using UnityEngine.UI;

public class Dmx_Sender : MonoBehaviour
{
    public byte[] DMXData = new byte[512];
    public InputField ip_textfield;

    ArtNet.Engine ArtEngine;
	Animator animator;

	// LED1
	public Light softwareLight1; // this color will be transmitted to LED 1 (and 2)
	public GameObject softwareLight1_debug;
	int port_led_1_master = 0; // DMX22
	int port_led_1_r = 1; // DMX23
	int port_led_1_g = 2; // DMX24
	int port_led_1_b = 3; // DMX25 (-> B ??? Channel 25 and up cause issues)
	Color prevLed_1_Color;


	// Shaker
	public Light shaker; 
	public GameObject shaker_debug;
	int port_shaker = 0;            // DMX 1
	Color prevShaker_Value;



    void Start()
    {
        for (int i = 0; i < DMXData.Length; i++)
        {
            DMXData[i] = (byte)(0);
        }

		DMXData[0] = (byte)(0);

        // Artnet sender / client
        string ipAddress = ip_textfield.text;
        if(ip_textfield.text == "") ipAddress = ip_textfield.placeholder.GetComponent<Text>().text;
        print(ipAddress);
        ArtEngine = new ArtNet.Engine("Open DMX Etheret", ipAddress);
        ArtEngine.Start();

		animator = GetComponent<Animator> ();
    }

		// UI buttons
	public void DMX_RightCombination(){
		
		animator.SetTrigger ("trg_RightCombination");
	}

	public void DMX_WrongCombination(){
		animator.SetTrigger ("trg_WrongCombination");
	}

	void Update(){
	// First change DMX array values (DMX Port, value [0..255]));
	// Then send it away.
	// Pick the current color of the light in the scene and send it to real LEDs via Artnet

		if (softwareLight1.color != prevLed_1_Color) {
			// 1 RGB LED needs 3 DMX addresses for each channel 
			DMXData[port_led_1_master] = (byte)(255);
			DMXData[port_led_1_r] = (byte)(softwareLight1.color.r * 255);
			DMXData[port_led_1_g] = (byte)(softwareLight1.color.g * 255);
			DMXData[port_led_1_b] = (byte)(softwareLight1.color.b * 255);

			// see color also on a gameobject in scene
			softwareLight1_debug.GetComponent<Renderer>().material.color = softwareLight1.color;
			prevLed_1_Color = softwareLight1.color;
		}

		if (shaker.color != prevShaker_Value) {
			DMXData[port_shaker] = (byte)(shaker.color.a * 255);

			// see color also on a gameobject in scene
			shaker_debug.GetComponent<Renderer>().material.color = shaker.color;
			prevShaker_Value = shaker.color;
		}


		ArtEngine.SendDMX(0, DMXData, DMXData.Length);
	}
}