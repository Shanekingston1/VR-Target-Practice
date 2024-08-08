using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using UnityEngine.InputSystem;

public class BulletHole : MonoBehaviour
{
    [SerializeField] private GameObject bulletHole;
   

    private float triggerValue;

    public InputActionReference TriggerInputActionReference;



    // Update is called once per frame
    void Update()
    {
        triggerValue = TriggerInputActionReference.action.ReadValue<float>();
       
        
            Debug.Log("Trigger button is pressed.");
            RaycastHit hitInfo; //Contains raycast hit info

            if(Physics.Raycast(transform.position, transform.forward, out hitInfo))
            {//Returns true if ray hits something
                GameObject obj = Instantiate(bulletHole, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                //Instantiate the bullet hole object.
                obj.transform.position += Quaternion.LookRotation(hitInfo.normal) * obj.transform.forward / 1000;  //Changing the bullet hole's position a bit so it will fit better
                                                                                                         //(will ensure that the sprite is moved away from the hit surface at any angle.)


            
        }
    }
}
