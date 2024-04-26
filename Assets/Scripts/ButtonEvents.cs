using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEvents : MonoBehaviour
{
	public void startButtonEvent()
	{
		SceneManager.LoadScene(1);

	}
	public void creditEvent()
	{
		SceneManager.LoadScene("Credits");

	}
	public void optionsEvent()
	{
		SceneManager.LoadScene("Options");
	}
	public void BackToHome()
	{
		SceneManager.LoadScene(0);
	}

	public void exitGame()
	{
        Application.Quit();
    }
}
