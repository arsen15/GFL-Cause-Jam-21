using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
        // Check if the current scene is NOT "Level 2"
        if (SceneManager.GetActiveScene().name != "Level_2")
        {
            // If not in "Level 2", respawn at the specified position
            transform.position = new Vector2(-13.0f, 0.0f);
        } else
		{
            transform.position = new Vector2(-58.0f, -9.0f);
        }
        health = maxHealth;
	}

	private void UpdateHealthBar()
	{
		healthBar.value = (float)health / maxHealth;
	}

}
