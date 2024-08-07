using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using System.Globalization;

[AddComponentMenu("Nokobot/Modern Guns/Simple Shoot")]
public class SimpleShoot : MonoBehaviour
{
    [Header("Prefab Refrences")]
    public GameObject bulletPrefab;
    public GameObject casingPrefab;
    public GameObject muzzleFlashPrefab;
    public TextMeshProUGUI AmmoText;
    

    [Header("Location Refrences")]
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private Transform barrelLocation;
    [SerializeField] private Transform casingExitLocation;


    [Header("Settings")]
    [Tooltip("Specify time to destory the casing object")][SerializeField] private float destroyTimer = 2f;
    [Tooltip("Bullet Speed")][SerializeField] private float shotPower = 200f;
    [Tooltip("Casing Ejection Speed")][SerializeField] private float ejectPower = 150f;

    public AudioSource source;
    public AudioClip fireSound;
    public AudioClip reload;
    public AudioClip reload2;
    public AudioClip noAmmo;
    public Magazine magazine;

    public XRBaseInteractor socketInteractor;

    public void AddMagazine(SelectEnterEventArgs args)
    {
        GameObject targetObject = args.interactableObject.transform.gameObject;
        Magazine magazine = targetObject.GetComponent<Magazine>();
        if (magazine != null)
        {
            this.magazine = magazine;
            source.PlayOneShot(reload2);
        }
    }
    public void RemoveMagazine(SelectExitEventArgs args)
    {
        magazine = null;
        source.PlayOneShot(reload);
    }

    void Start()
    {
        magazine = null;
        if (barrelLocation == null)
            barrelLocation = transform;

        if (gunAnimator == null)
            gunAnimator = GetComponentInChildren<Animator>();

        socketInteractor.selectEntered.AddListener(args => { AddMagazine(args); });
        socketInteractor.selectExited.AddListener(args => { RemoveMagazine(args); });
    }

    private void Update()
    {
        if (magazine != null)
        {
            AmmoText.text = magazine.numberOfBullet.ToString();
        }
        else
        {
            AmmoText.text = "0";
        }
    }

    public void PulltheTrigger()
    {
        if (magazine && magazine.numberOfBullet > 0)
        {
            gunAnimator.SetTrigger("Fire");
        }
        else
        {
            source.PlayOneShot(noAmmo);
        }
    }

    //This function creates the bullet behavior
    // This function creates the bullet behavior
    public void Shoot()
    {
        // Create a new bullet
        GameObject bullet = Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation);

        // Get the Rigidbody component of the bullet
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

        // Calculate the direction of the shot
        Vector3 shotDirection = barrelLocation.forward;

        // Add a small random deviation to the shot direction to simulate bullet spread
        shotDirection = Quaternion.Euler(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0) * shotDirection;

        // Calculate the initial velocity of the bullet
        Vector3 initialVelocity = shotDirection * shotPower;

        // Add the initial velocity to the bullet
        bulletRigidbody.velocity = initialVelocity;

        // Create a new muzzle flash
        GameObject muzzleFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);

        // Destroy the muzzle flash after a short time
        Destroy(muzzleFlash, 0.1f);

        // Create a new casing
        CasingRelease();

        // Play the fire sound
        source.PlayOneShot(fireSound);

        // Reduce the number of bullets in the magazine
        magazine.numberOfBullet--;
    }

    // This function creates a casing at the ejection slot
    void CasingRelease()
    {
        // Cancels function if ejection slot hasn't been set or there's no casing
        if (!casingExitLocation || !casingPrefab)
        { return; }

        // Create the casing
        GameObject casing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation);

        // Get the Rigidbody component of the casing
        Rigidbody casingRigidbody = casing.GetComponent<Rigidbody>();

        // Calculate the ejection direction
        Vector3 ejectionDirection = casingExitLocation.right;

        // Add a small random deviation to the ejection direction
        ejectionDirection = Quaternion.Euler(0, Random.Range(-30f, 30f), 0) * ejectionDirection;

        // Calculate the initial velocity of the casing
        Vector3 initialVelocity = ejectionDirection * ejectPower;

        // Add the initial velocity to the casing
        casingRigidbody.velocity = initialVelocity;

        // Add a torque to the casing to make it spin
        casingRigidbody.AddTorque(ejectionDirection * 100f, ForceMode.Impulse);

        // Destroy the casing after a short time
        Destroy(casing, destroyTimer);
    }


}
