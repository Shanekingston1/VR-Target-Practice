using UnityEngine;

public class CollisionIgnorer : MonoBehaviour
{
    void Start()
    {
        // Get the colliders
        Collider gunCollider = GetComponent<Collider>();
        Collider magazineCollider = GameObject.FindWithTag("GunMagazine").GetComponent<Collider>();

        // Ignore collisions between the two colliders
        Physics.IgnoreCollision(gunCollider, magazineCollider);
    }
}