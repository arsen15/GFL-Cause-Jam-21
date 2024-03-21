using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
	public int damage = 10;
	public float speed = 20.0f;
	public Rigidbody2D rb;

	// Start is called before the first frame update
	void Start()
	{

		rb = GetComponent<Rigidbody2D>();

		Vector3 a = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3 dir = a - transform.position; //direction
		
		
		//this checks to make sure that your not shooting behind u 
		if (sameSign(dir.x, transform.right.x) && sameSign(dir.y, transform.right.y))
			rb.velocity = dir.normalized * speed;
		else
			Destroy(gameObject);
	}

	// Update is called once per frame
	void Update()
	{

		Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
		if (screenPoint.x < 0 || screenPoint.x > Screen.width || screenPoint.y < 0 || screenPoint.y > Screen.height)
		{
			Destroy(gameObject);
		}
	}
	private void OnTriggerEnter2D(Collider2D hit)
	{
		Enemy1 enemy = hit.GetComponent<Enemy1>();
		if (enemy != null)
		{
			enemy.TakeDamage(damage);
		}
		Destroy(gameObject);
	}
	private bool sameSign(float num1, float num2)
	{
		return num1 >= 0 && num2 >= 0 || num1 < 0 && num2 < 0;
	}
}
