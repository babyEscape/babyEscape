using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public GameObject crate; // Assign the cube GameObject in the Inspector
    public float forceMagnitude = 250f; // Adjust the force as needed
    public float delay = 3;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dummy"))
        {
            // play sound here
            Invoke("LaunchCrate", delay);
        }
    }

    private void LaunchCrate()
    {
        Rigidbody rb = crate.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = true; // Enable gravity
            Vector3 forceDirection = Vector3.left;
            rb.AddForce(forceDirection * forceMagnitude, ForceMode.Impulse);
        }
        else
        {
            Debug.LogWarning("No Rigidbody found on the crate.");
        }
    }
}

