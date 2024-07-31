using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 10f;
    public Transform tip;

    private Rigidbody _rigidbody;
    private bool inAir = false;
    private Vector3 lastPostion = Vector3.zero;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        PullInteraction.PullActionReleased += Release;

        Stop();
    }

    private void OnDestroy()
    {
        PullInteraction.PullActionReleased -= Release;
    }

    private void Release(float value)
    {
        PullInteraction.PullActionReleased -= Release;
        gameObject.transform.parent = null;
        inAir = true;
        SetPhysics(true);

        Vector3 force = transform.forward * value * speed;
        _rigidbody.AddForce(force, ForceMode.Impulse);

        StartCoroutine(RotateWithVelocity());

        lastPostion = tip.position;
    }

    private IEnumerator RotateWithVelocity()
    {
        yield return new WaitForFixedUpdate();
        while(inAir)
        {
            Quaternion newRotation = Quaternion.LookRotation(_rigidbody.velocity, transform.up);
            transform.rotation = newRotation;
            yield return null;
        }
    }

    private void FixedUpdate()
    {
        if (inAir)
        {
            CheckCollision();
            lastPostion= tip.position;
        }
    }

    private void CheckCollision()
    {
        if (Physics.Linecast(lastPostion, tip.position, out RaycastHit hitInfo))
        {
            if(hitInfo.transform.gameObject.layer != 8)
            {
                if(hitInfo.transform.TryGetComponent(out Rigidbody body))
                {
                    _rigidbody.interpolation = RigidbodyInterpolation.None;
                    transform.parent = hitInfo.transform;
                    body.AddForce(_rigidbody.velocity, ForceMode.Impulse);
                }
                Stop();
            }
        }
    }

    private void Stop()
    {
        inAir = false;
        SetPhysics(false);
    }
    
    private void SetPhysics(bool usePhysics)
    {
        _rigidbody.useGravity = usePhysics;
        _rigidbody.isKinematic = usePhysics;
    }
}
