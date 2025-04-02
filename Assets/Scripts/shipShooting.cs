using UnityEngine;

public class ShipShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // Assign the bullet prefab in the Inspector
    public Transform firePoint; // Assign a fire point (empty GameObject positioned at the ship's blaster)
    public float fireRate = 0.2f; // Delay between shots
    private float nextFireTime = 0f;

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
