using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
	// Start is called before the first frame update
	public GameObject PausePanel;

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			PausePanel.SetActive(!PausePanel.activeSelf);
			Time.timeScale = 1 - Time.timeScale;
		}

	}
}
