using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoundManager : MonoBehaviour
{
	public Options options;
	//public Slider slider;

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


    // Update is called once per frame
    void Update()
	{
		//options.SoundAdjustment = (int)slider.value; //Idk why this isnt interactable but should in theory work 
	}

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
