using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoundManager : MonoBehaviour
{
	[SerializeField] Slider volumeSlider;
	void Start()
	{
		if(!PlayerPrefs.HasKey("musicVolume"))
		{
			PlayerPrefs.SetFloat("musicVolume", 1);
			Load();
		}

		else
		{
			Load();
		}
	}
	public void ChangeVolume()
	{
		AudioListener.volume = volumeSlider.value;
		Save();
	}
	
	private void Load()
	{
		volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
	}

	private void Save()
	{
		PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
	}

	[Header("---------------- Audio Source -------------------")]
	[SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("---------------- Audio Clip -------------------")]
    public AudioClip boarWalk;
    public AudioClip boarHurt;

	public AudioClip snakeHiss;
    public AudioClip snakeSpit;

    public AudioClip playerInjured;
    public AudioClip playerRespawn;
	public AudioClip weaponPickup;
	public void PlayBoarWalk()
	{
		musicSource.clip = boarWalk;
		musicSource.Play();
	}

	public void PlaySFX(AudioClip clip)
	{
		SFXSource.PlayOneShot(clip);
	}
}
