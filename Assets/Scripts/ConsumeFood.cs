using System.Collections;
using UnityEngine;

public class EatFood : MonoBehaviour
{
    [Header("Food Related Settings")]
    public ParticleSystem eatingParticles; 
    public AudioClip eatingSound;          
    public Transform mouthPosition;        
    public GameObject canvasObject;
    
    [Header("UI Settings")]
    public float uiDuration;
    public float uiStartDuration;
    
    private AudioSource audioSource;       
    
    [Header("Locomotion File")]
    public MonoBehaviour climbLocomotion;
    private void Start()
    {
        
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Food"))
        {
            Eat(other.gameObject);
        }
    }

    private void Eat(GameObject food)
    {
        
        if (eatingParticles != null)
        {
            eatingParticles.Play();
        }

        
        if (eatingSound != null)
        {
            audioSource.PlayOneShot(eatingSound);
        }

        
        Destroy(food);

        if (climbLocomotion != null)
        {
            climbLocomotion.enabled = true;
        }

        
        StartCoroutine(FadeCanvasSequence());
    }

    private IEnumerator FadeCanvasSequence()
    {
        CanvasGroup canvasGroup = canvasObject.GetComponent<CanvasGroup>();
        
        yield return new WaitForSeconds(uiStartDuration);
    
        // Fade in
        yield return StartCoroutine(FadeCanvasGroup(canvasGroup, 0f, 1f, 1));
        
        // Wait for UI display time
        yield return new WaitForSeconds(uiDuration);
        
        // Fade out
        yield return StartCoroutine(FadeCanvasGroup(canvasGroup, 1f, 0f, 1));
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
