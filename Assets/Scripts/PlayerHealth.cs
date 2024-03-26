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
			// Destroy(gameObject);
			respawn();
		}

	}
	public void respawn()
	{
		transform.position = new Vector2(-13.0f, 0.0f);
		healthBar.value = maxHealth;
		health = maxHealth;
	}

}
