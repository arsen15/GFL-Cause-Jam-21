using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayerScripy : MonoBehaviour
{
	// Update is called once per frame
	public bool isPLaying;
	void Update()
	{
		if (!PauseMenu.GameIsPaused)
		{
			if (Input.GetAxisRaw("Horizontal") != 0)
			{

				if (!isPLaying)
				{
					GetComponent<AudioSource>().Play();

					isPLaying = true;
				}
			}
			else
			{
				GetComponent<AudioSource>().Stop();
				isPLaying = false;
			}
		}
	}
}
