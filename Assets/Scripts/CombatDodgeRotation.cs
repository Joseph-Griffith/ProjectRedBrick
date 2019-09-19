using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatDodgeRotation : MonoBehaviour
{
    PlayerMotor playerMotor;
    PlayerController player;

    RaycastHit hit = new RaycastHit();

    float tempXHit;
    float tempZHit;

    // Use this for initialization
    private void Start()
    {
        playerMotor = GetComponentInParent<PlayerMotor>();
        player = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if (player.WeaponSheathed)
        {
            // Smoothly rotate from the current position to the players current rotation
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(playerMotor.transform.forward, playerMotor.transform.up), 0.15f);
        }

        if (!player.WeaponSheathed)
        {
            // Get the forward and horizontal vector of the camera in world space
            Vector3 cameraForward = Camera.main.transform.forward.normalized;
            Vector3 cameraHorizontal = Camera.main.transform.right.normalized;

            // Zero the y axis
            cameraForward.y = 0;
            cameraHorizontal.y = 0;

            // Get a ray from the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Create a new layermask of the ground layer
            LayerMask groundMask = LayerMask.GetMask("Ground");

            // If the ray hits the ground layer
            if (Physics.Raycast(ray, out hit, 100, groundMask)) // Return the RaycastHit information
            {
                // Rotate the player to look at at current mouse position in the x and z direction whilst maintaining the current y position
                this.transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
            }
        }
    }
}
