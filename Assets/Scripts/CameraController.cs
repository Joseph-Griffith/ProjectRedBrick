using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float pitch = 2f;
    public float zoomSpeed = 4f;
    public float minZoom = 5f;
    public float maxZoom = 15f;
    public float zoomTime = 0.5f;

    private float currentZoom = 10f;

    // Update is called once per frame
    private void Update()
    {
        // Zoom the camera in or out at the zoomSpeed
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;

        // Clamp the zoom
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
    }

    // Called at the end of each frame
    private void LateUpdate()
    {
        //// Move to the targets position but maintain a certain distance whilst dynamically moving depending on the way the player is moving
        //this.transform.position = Vector3.Lerp(transform.position, target.position - offset * currentZoom, zoomTime * Time.deltaTime);

        // Move to the targets position but maintain a certain distance
        this.transform.position = target.position - offset * currentZoom; // Remember to set the zoom speed to 10 in the inspector

        // Look at the target
        this.transform.LookAt(target.position + Vector3.up * pitch);
    }
}
