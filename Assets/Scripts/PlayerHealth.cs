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
		UpdateHealthBar();

	}
	private void Update()
	{

		UpdateHealthBar();

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

	public void Heal(int healAmount)
	{
		health += healAmount;
		if (health > maxHealth)
		{
			health = maxHealth;
		}
	}
	public void respawn()
	{
		transform.position = new Vector2(-13.0f, 0.0f);
		health = maxHealth;
	}

	private void UpdateHealthBar()
	{
		healthBar.value = (float)health / maxHealth;
	}

}
