using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public int damage = 10;
    public float speed = 20.0f;
    public Rigidbody2D rb;

    //Control when bullet should destroy on collision
    private bool destroyOnCollision = true;
    public AudioSource onCollisonSound;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Vector3 a = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dir = a - transform.position; //direction
        // Debug.Log("transform: " + transform.right);
        // Debug.Log("dir: " + dir);
        //this checks to make sure that your not shooting behind u
        if (SameSign(dir.x, transform.right.x))
            rb.velocity = dir.normalized * speed;
        else
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        if (
            screenPoint.x < 0
            || screenPoint.x > Screen.width
            || screenPoint.y < 0
            || screenPoint.y > Screen.height
        )
        {
            Destroy(gameObject);
        }
    }

    public void InitializeBullet(bool destroyOnImpact = true)
    {
        destroyOnCollision = destroyOnImpact;

        Vector3 a = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dir = a - transform.position; //direction
        GetComponent<AudioSource>().Play();
        if (SameSign(dir.x, transform.right.x))
        {
            rb.velocity = dir.normalized * speed;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D hit)
    {
        Enemy1 enemy = hit.GetComponent<Enemy1>();
        // onCollisonSound.Play();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        Boar boar = hit.GetComponent<Boar>();
        if (boar != null)
        {
            boar.TakeDamage(damage);
            boar.SetChasing(true);
        }

        // Boar boar = hit.GetComponent<Boar>();
        // if (boar != null) {
        // 	boar.TakeDamage(damage);
        // 	boar.SetChasing(true);
        // } //resolve

        if (
            hit.gameObject.CompareTag("Collectible Item")
            || hit.gameObject.CompareTag("Enemy Bullet")
        )
        {
            Debug.Log("Hit collectible and enemy bullet");
            return;
        }

        Destroy(gameObject);
    }

    private bool SameSign(float num1, float num2)
    {
        return num1 >= 0 && num2 >= 0 || num1 < 0 && num2 < 0;
    }
}
