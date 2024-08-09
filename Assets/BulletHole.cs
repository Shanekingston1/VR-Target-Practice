using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BulletHole : MonoBehaviour
{
    
   [SerializeField] public GameObject decalPrefab;

    public void BulletImpact(Vector3 impactPosition, Vector3 impactNormal)
    {
        
            Vector3 pointerPosition = transform.position;
            Ray ray = new Ray (pointerPosition, transform.forward);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, 100f))
            {
                Instantiate(decalPrefab, hitInfo.point, Quaternion.FromToRotation(Vector3.up, hitInfo.normal));
            }
        
    }
}
