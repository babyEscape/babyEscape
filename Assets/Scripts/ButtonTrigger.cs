using System.Collections;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public GameObject crate; // Assign the cube GameObject in the Inspector
    public GameObject washer; // Assign the object that will move sporadically
    public float forceMagnitude = 250f; // Adjust the force as needed
    public float movementDelay = 0.5f; // Delay before shaking washer
    
    public AudioClip machineBoot;
    public AudioClip machineOn;
    private AudioSource audioSource;

    private float delay;
    private bool hasTriggered = false;

    void Start()
    {
        // Create an AudioSource on this GameObject if one doesn’t already exist
        audioSource = gameObject.AddComponent<AudioSource>();
        delay = machineOn.length - 1; // Delay before launching the crate
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered) return;
        hasTriggered = true; 
        
        audioSource.PlayOneShot(machineBoot);
        
        if (other.CompareTag("Dummy"))
        {
            // Start moving the object sporadically for 1 second
            StartCoroutine(MoveObjectSporadically());

            // After the movement, invoke the LaunchCrate function
            Invoke("LaunchCrate", delay);
        }
    }

    private IEnumerator MoveObjectSporadically()
    {
        yield return new WaitForSeconds(movementDelay);
        
        audioSource.PlayOneShot(machineOn);
        
        float timeElapsed = 0f;
        Vector3 originalPosition = washer.transform.position;

        while (timeElapsed < delay)
        {
            // Move the object in a random direction
            Vector3 randomDirection = new Vector3(
                Random.Range(-0.02f, 0.02f),  // Random X
                Random.Range(-0.02f, 0.02f),  // Random Y
                Random.Range(-0.02f, 0.02f)   // Random Z
            );

            // Move object sporadically (this will apply small random movements)
            washer.transform.position = originalPosition + randomDirection;

            // Update timeElapsed
            timeElapsed += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Ensure the object returns to its original position after 1 second
        washer.transform.position = originalPosition;
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
