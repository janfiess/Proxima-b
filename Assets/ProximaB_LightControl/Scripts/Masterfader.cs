// script attached to the Manager GameObject which is always available in scene

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Masterfader: MonoBehaviour
{

    public Slider masterfader;
    public Text masterfader_info;
    public float prev_val = 0;
	public float val;

    void Update()
    {
        // brightness slider
        if (masterfader.value == prev_val) return;
		val = masterfader.value;
        masterfader_info.text = val.ToString();
        prev_val = val;
        print("masterfader: " + val.ToString());
    }

	public void FadeUp(){

	}

	public void FadeDown(){

	}
}
