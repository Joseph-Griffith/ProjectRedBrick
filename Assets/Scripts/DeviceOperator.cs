using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeviceOperator : MonoBehaviour
{
    public float radius = 5f;

    // Update is called once per frame
    private void Update()
    {
        // If the 'E' key is pressed...
		if (Input.GetKeyDown(KeyCode.E))
        {
            // Get a list of objects that fall within the radius of our sphere
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

            // For each object found...
            foreach (Collider hitCollider in hitColliders)
            {
                // Try to call the "Operate" method if that object has one
                hitCollider.SendMessage("Operate", SendMessageOptions.DontRequireReceiver);

                //// Get the direction from the player to the object 
                //Vector3 direction = hitCollider.transform.position - transform.position;

                //// If the operator is facing the direction of the object...
                //if (Vector3.Dot(transform.forward, direction) > 0.5f)
                //{
                //    // Try to call the "Operate" method if that object has one
                //    hitCollider.SendMessage("Operate", SendMessageOptions.DontRequireReceiver);
                //}
            }
        }
    }
}
