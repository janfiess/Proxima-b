// attached to the Manager GameObject

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefaultBrightness : MonoBehaviour {

	string texxt;
    public float val;
	float prev_val;
    public InputField textfield;
    public Slider slider;

	void Awake()
    {
        texxt = textfield.placeholder.GetComponent<Text>().text;
        val = float.Parse(texxt);
        print("default_brightness_textfield.text: " + val);
    }

	 void Update()
    {
        /**
        Get default brightness value from textfield and detect if the value changes
         */
        if (textfield.text != "" && textfield.text != texxt)
        {
            texxt = textfield.text;
            val = float.Parse(texxt);
            slider.value = val;
            print("default_brightness_val changed: " + val);
        }
        else if(textfield.text == "" && textfield.placeholder.GetComponent<Text>().text != texxt){
            texxt = textfield.placeholder.GetComponent<Text>().text;
            val = float.Parse(texxt);
            slider.value = val;
            print("default_brightness_val changed: " + val);
        }


    }
}
