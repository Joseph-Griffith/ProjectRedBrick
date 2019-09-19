using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenDevice : MonoBehaviour
{
    [SerializeField] private Vector3 dPos;

    private bool open;
    private Vector3 pos;

    /// <summary>
    /// Opens or closes the door
    /// </summary>
	public void Operate()
    {
        if (open)
        {
            // Calculate the door open position
            pos = transform.position - dPos;

            // Open the door
            transform.position = pos; // TODO: Tween the door opening
        }
        else
        {
            // Calculate the door close position
            pos = transform.position + dPos;

            // Close the door
            transform.position = pos; // TODO: Tween the door closing
        }

        // Set the door open boolean to the opposite of what it was
        open = !open;
    }
}
