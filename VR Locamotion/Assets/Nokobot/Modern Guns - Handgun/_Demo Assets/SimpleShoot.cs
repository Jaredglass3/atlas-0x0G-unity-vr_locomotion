using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Nokobot/Modern Guns/Simple Shoot")]
public class SimpleShoot : MonoBehaviour
{
    [Header("Prefab References")]
    public GameObject bulletPrefab;
    public GameObject casingPrefab;
    public GameObject muzzleFlashPrefab;

    [Header("Location References")]
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private Transform barrelLocation;
    [SerializeField] private Transform casingExitLocation;

    [Header("Settings")]
    [Tooltip("Specify time to destroy the casing object")] 
    [SerializeField] private float destroyTimer = 2f;
    [Tooltip("Bullet Speed")] 
    [SerializeField] private float shotPower = 500f;
    [Tooltip("Casing Ejection Speed")] 
    [SerializeField] private float ejectPower = 150f;
    [Tooltip("Damage Amount")] 
    [SerializeField] private int damageAmount = 10;

    void Start()
    {
        if (barrelLocation == null)
            barrelLocation = transform;

        if (gunAnimator == null)
            gunAnimator = GetComponentInChildren<Animator>();
    }

    public void TriggerPull()
    {
        gunAnimator.SetTrigger("Fire");
    }

    // This function creates the bullet behavior
    void Shoot()
    {
        if (muzzleFlashPrefab)
        {
            // Create the muzzle flash
            GameObject tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);
            // Destroy the muzzle flash effect
            Destroy(tempFlash, destroyTimer);
        }

        // Cancels if there's no bullet prefab
        if (!bulletPrefab)
        { 
            return; 
        }

        // Create a bullet and add force on it in direction of the barrel
        Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation).GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);

// Perform a raycast to detect if a zombie is hit
RaycastHit hit;
Debug.Log("Performing raycast from " + barrelLocation.position + " in direction " + barrelLocation.forward);

if (Physics.Raycast(barrelLocation.position, barrelLocation.forward, out hit))
{
    Debug.Log("Raycast hit something: " + hit.transform.name);

    if (hit.transform.CompareTag("Zombie"))
    {
        Debug.Log("Hit a zombie: " + hit.transform.name);
        
        ZombieHealth zombieHealth = hit.transform.GetComponent<ZombieHealth>();
        if (zombieHealth != null)
        {
            Debug.Log("Zombie has ZombieHealth component, applying damage: " + damageAmount);
            zombieHealth.TakeDamage(damageAmount);
        }
        else
        {
            Debug.LogWarning("Zombie does not have a ZombieHealth component.");
        }
    }
    else
    {
        Debug.Log("Hit object is not a zombie: " + hit.transform.name);
    }
}
else
{
    Debug.Log("Raycast did not hit anything.");
}
    }

    // This function creates a casing at the ejection slot
    void CasingRelease()
    {
        // Cancels function if ejection slot hasn't been set or there's no casing
        if (!casingExitLocation || !casingPrefab)
        { 
            return; 
        }

        // Create the casing
        GameObject tempCasing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation) as GameObject;
        // Add force on casing to push it out
        tempCasing.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(ejectPower * 0.7f, ejectPower), 
            (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
        // Add torque to make casing spin in random direction
        tempCasing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)), ForceMode.Impulse);

        // Destroy casing after X seconds
        Destroy(tempCasing, destroyTimer);
    }
}
