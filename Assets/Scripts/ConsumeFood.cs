using System.Collections;
using UnityEngine;
public class EatFood : MonoBehaviour
{
    public ParticleSystem eatingParticles; // Drag and drop your particle system prefab
    public AudioClip eatingSound;          // Drag and drop your eating sound
    public Transform mouthPosition;        // Reference to where the "mouth" is in the XR rig
    public GameObject canvasObject;
    private AudioSource audioSource;       // Audio source for playing the sound
    
    public MonoBehaviour climbLocomotion;
    private void Start()
    {
        // Create an AudioSource on this GameObject if one doesn’t already exist
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object is tagged as Food
        if (other.CompareTag("Food"))
        {
            Eat(other.gameObject);
        }
    }

    private void Eat(GameObject food)
    {
        // Play particles at the mouth position
        if (eatingParticles != null)
        {
            eatingParticles.Play();
        }

        // Play the eating sound
        if (eatingSound != null)
        {
            audioSource.PlayOneShot(eatingSound);
        }

        // Destroy the food object
        Destroy(food);

        if (climbLocomotion != null)
        {
            climbLocomotion.enabled = true;
        }

        StartCoroutine(FadeAfterCanvasGroup());
    }

    private IEnumerator FadeAfterCanvasGroup()
    {
        yield return new WaitForSeconds(5f);
        StartCoroutine(FadeCanvasGroup(canvasObject.GetComponent<CanvasGroup>(),1f, 0f, 2f));
    }
    
    private IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float start, float end, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, elapsedTime / duration);
            yield return null;
        }
        
        canvasGroup.alpha = end;
    }
}