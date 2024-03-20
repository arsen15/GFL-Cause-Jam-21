using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAndShoot : MonoBehaviour
{
    public GameObject player;
    public GameObject crosshairs;
    public GameObject playerBulletPrefab;
    public float playerBulletSpeed = 50.0f;
    public Transform BulletStart;

    private Vector3 target;
    private bool canFire = true;

    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        target = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        crosshairs.transform.position = new Vector2(target.x, target.y);

        Vector3 difference = target - player.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        if (Input.GetMouseButtonDown(0) && canFire)
        {
            float distance = difference.magnitude;
            Vector2 direction = difference / distance;
            direction.Normalize();
            FireBullet(direction, rotationZ);
            canFire = false; // Prevent firing until the mouse button is released
        }

        if (Input.GetMouseButtonUp(0))
        {
            canFire = true; // Allow firing again after the mouse button is released
        }
    }

    void FireBullet(Vector2 direction, float rotationZ)
    {
        GameObject b = Instantiate(playerBulletPrefab) as GameObject;
        b.transform.position = BulletStart.position;
        b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

        if (player.transform.localScale.x < 0)
        {
            direction = -direction;
            b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ + 180f);
        }

        b.GetComponent<Rigidbody2D>().velocity = direction * playerBulletSpeed;
    }

}
