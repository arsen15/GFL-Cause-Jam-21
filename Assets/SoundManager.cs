using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoundManager : MonoBehaviour
{
	public Options options;
	public Slider slider;
	// Start is called before the first frame update


	// Update is called once per frame
	void Update()
	{
		options.SoundAdjustment = (int)slider.value; //Idk why this isnt interactable but should in theory work 
	}
}
