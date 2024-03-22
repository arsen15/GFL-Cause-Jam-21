using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
	public int maxHealth = 10;
	public int health;

	public Slider healthBar; //use slider prefab 
	void Start()
	{
		health = maxHealth;
		healthBar.maxValue = 1;
		healthBar.minValue = 0;
		healthBar.value = 1;

	}
	private void Update()
	{

		healthBar.value = (float)health / maxHealth;

	}

	public void TakeDamage(int damage)
	{
		health -= damage;

		if (health <= 0)
		{
			Destroy(gameObject);
			healthBar.value = 0;
			health = 0;
		}

	}

}
