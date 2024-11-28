using System.Collections;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public GameObject crate; 
    public GameObject washer; 
    public float forceMagnitude = 250f; 
    public float movementDelay = 0.5f; 
    
    public AudioClip machineBoot;
    public AudioClip machineOn;
    private AudioSource audioSource;

    private float delay;
    private bool hasTriggered = false;

    void Start()
    {
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

            StartCoroutine(MoveObjectSporadically());

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
            Vector3 randomDirection = new Vector3(
                Random.Range(-0.02f, 0.02f),  // Random X
                Random.Range(-0.02f, 0.02f),  // Random Y
                Random.Range(-0.02f, 0.02f)   // Random Z
            );

            washer.transform.position = originalPosition + randomDirection;

            timeElapsed += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        washer.transform.position = originalPosition;
    }

    private void LaunchCrate()
    {
        Rigidbody rb = crate.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = true; 
            Vector3 forceDirection = Vector3.left;
            rb.AddForce(forceDirection * forceMagnitude, ForceMode.Impulse);
        }
        else
        {
            Debug.LogWarning("No Rigidbody found on the crate.");
        }
    }
}
