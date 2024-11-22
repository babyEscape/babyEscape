using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public GameObject cube; // Assign the cube GameObject in the Inspector
    public float forceMagnitude = 10f; // Adjust the force as needed

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dummy"))
        {
            Rigidbody cubeRb = cube.GetComponent<Rigidbody>();
            if (cubeRb != null)
            {
                cubeRb.useGravity = true; // Enable gravity
                Vector3 forceDirection = Vector3.left;
                cubeRb.AddForce(forceDirection * forceMagnitude, ForceMode.Impulse);
            }
            else
            {
                Debug.LogWarning("No Rigidbody found on the cube.");
            }
        }
    }
}

