using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed;
    public int startingPoint;
    public Transform[] points;

    private int i; // Index of the array
    private bool isFrozen = false; // Flag to check if the platform is frozen

    // Start is called before the first frame update
    void Start()
    {
        transform.position = points[startingPoint].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFrozen) // Check if the platform is not frozen
        {
            if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
            {
                i++;
                if (i == points.Length)
                {
                    i = 0;
                }
            }
            transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Handling player collision to parent the player
        if (collision.collider.CompareTag("Player"))
        {
            if (transform.position.y < collision.transform.position.y - 0.8f)
            {
                collision.transform.SetParent(transform);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Icicle"))
        {
            StartCoroutine(FreezePlatform());
            // Optionally, destroy the bullet if it should disappear upon hitting the platform
            Destroy(other.gameObject);
        }
    }

    private IEnumerator FreezePlatform()
    {
        isFrozen = true;
        yield return new WaitForSeconds(3); // Freeze time duration
        isFrozen = false;
    }
}
