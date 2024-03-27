using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCollision : MonoBehaviour
{
    private bool isPlayerInLaser = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPlayerInLaser)
        {
            Debug.Log("Player hit by laser!");
            isPlayerInLaser = true; // Set flag to true as player is now in the laser.
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInLaser = false; // Reset flag when player exits the laser.
            Debug.Log("Player is safe!");
        }
    }

    // Optionally use OnTriggerStay if you want to continuously apply an effect.
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Continuously apply an effect here, such as draining battery.
            Debug.Log("Player continuously hit by laser!");
        }
    }
}
