// script attached to the Manager GameObject which is always available in scene

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderToLights : MonoBehaviour
{
    public Slider slider;
    
    float prev_sliderVal;
	public Toggle graph_linear, graph_gammaCorrection, graph_gammaCorrection2, graph_ducks, graph_linearSteepStart, graph_linearSteepStart2, graph_linear2, graph_linear3, graph_linear4, graph_linear5, graph_linear6;
    public delegate byte FadingShape(float x);
    FadingShape fadingShape_function;
    FadingShapes fadingShapes;
    Masterfader masterfader;
    public Dmx_Sender dmxSender;
    public int dmxStartAddress = 0;
    // public GameObject debugLight1;
    public Transform debugLights;
    int debugLights_count;
   

    void Start()
    {
        fadingShapes = GetComponent<FadingShapes>();
        masterfader = GetComponent<Masterfader>();
        debugLights_count = debugLights.childCount;
    }

    void Update()
    {
        //if (slider.value == prev_sliderVal && masterfader.val == masterfader.prev_val) return;
        
        float sliderVal = slider.value * 1.8f; // adapt slider
        prev_sliderVal = sliderVal;
		// print("sliderVal: " + sliderVal.ToString() + " | prev_sliderVal: " + prev_sliderVal);
  
        

     
        // specify the fading function (fadingShape_function) depending on the selected radio button)
        if (graph_linear.isOn) fadingShape_function = fadingShapes.Linear;
        else if (graph_gammaCorrection.isOn) fadingShape_function = fadingShapes.GammaCorrection;
		else if (graph_linearSteepStart.isOn) fadingShape_function = fadingShapes.LinearSteepStart;
		else if (graph_linearSteepStart2.isOn) fadingShape_function = fadingShapes.LinearSteepStart2;
		else if (graph_linear2.isOn) fadingShape_function = fadingShapes.Linear2;
		else if (graph_linear3.isOn) fadingShape_function = fadingShapes.Linear3;
		else if (graph_linear4.isOn) fadingShape_function = fadingShapes.Linear4;
		else if (graph_linear5.isOn) fadingShape_function = fadingShapes.Linear5;
		else if (graph_linear6.isOn) fadingShape_function = fadingShapes.Linear6;
		// else if (graph_gammaCorrection2.isOn) fadingShape_function = fadingShapes.GammaCorrection2;
		else if (graph_ducks.isOn) fadingShape_function = fadingShapes.Ducks;
    
        // for each light
        // for (int i = 0; i < debugLights_count; i++)

		for (int i = debugLights_count-1; i >= 0; i--)
        {
            // execute the fading function fadingShape_function
            byte y = fadingShape_function(sliderVal-0.01f *i);
            // print("y-Value: " + y);

            // send value to light
            dmxSender.DMXData[dmxStartAddress + i] = y;

            // send value to software light
            Color debugLightColor = new Color((float)y / 255.0f, (float)y / 255, (float)y / 255, 1.0f);
            // print("debugLightColor: " + debugLightColor);
        	debugLights.GetChild(i).GetComponent<Renderer>().material.color = debugLightColor;
            
		}
    }
}
