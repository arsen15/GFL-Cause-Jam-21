using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    public Transform bulletStart;
    //public GameObject bulletPrefab;

    private InventoryManager inventoryManager;    

    private void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        if (inventoryManager == null)
        {
            Debug.LogError("No inventory manager!");

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        } 
    }

    void Shoot()
    {
        GameObject bulletPrefab = inventoryManager.GetSelectedBulletPrefab();
        if (bulletPrefab != null)
        {
            GameObject bulletObject = Instantiate(bulletPrefab, bulletStart.position, bulletStart.rotation);
            PlayerBullet bulletScript = bulletObject.GetComponent<PlayerBullet>();
            if (bulletScript != null)
            {
                bulletScript.InitializeBullet();
            }
        }
        else
        {
            Debug.Log("No bullet prefab found for the selected ammo type.");
        }
    }

}
