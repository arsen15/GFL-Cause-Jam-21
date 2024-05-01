using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEvents : MonoBehaviour
{
	public void startButtonEvent()
	{
        ResetTimeScale();
        PauseMenu.GameIsPaused = false;
        SceneManager.LoadScene(1);

	}
	public void creditEvent()
	{
        ResetTimeScale();
        PauseMenu.GameIsPaused = false;
        SceneManager.LoadScene("Credits");

	}
	public void optionsEvent()
	{
        ResetTimeScale();
        PauseMenu.GameIsPaused = false;
        SceneManager.LoadScene("Options");
	}
	public void BackToHome()
	{
        ResetTimeScale();
        PauseMenu.GameIsPaused = false;
        SceneManager.LoadScene(0);
	}

	public void exitGame()
	{
        Application.Quit();
    }

	private void ResetTimeScale()
	{
		Time.timeScale = 1f;
	}
}
