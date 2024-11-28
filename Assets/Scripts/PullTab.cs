using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class PullTab : MonoBehaviour
{
    public GameObject stringRenderer;
    public Transform pullTabGrabObject, objectParent;
    public float stringStretchLimit;
    public AudioClip toySound1, toySound2, toySound3, toySound4;
    public GameObject canvasObject;
    
    public float uiDuration;
    public float uiStartDuration;
    
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable interactable;
    private UnityEngine.XR.Interaction.Toolkit.Interactors.IXRSelectInteractor interactor;
    private AudioSource audioSource;
    private AudioClip[] audioClips = new AudioClip[4];
    private String[] subtitles = new String[4];
    private int toypulled = 0;

    private CanvasGroup canvasGroup; 
    private TMP_Text textMesh;
    
    private void Awake()
    {
        interactable = pullTabGrabObject.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        canvasGroup = canvasObject.GetComponent<CanvasGroup>();
        textMesh = canvasGroup.GetComponentInChildren<TMP_Text>();
    }

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioClips[0] = toySound1;
        audioClips[1] = toySound2;
        audioClips[2] = toySound3;
        audioClips[3] = toySound4;
        interactable.selectEntered.AddListener(PrepareString);
        interactable.selectExited.AddListener(ResetString);
        
        subtitles[0] = "That was shocking";
        subtitles[1] = "Remember children, always stick forks in electrical sockets";
        subtitles[2] = "You should stick a fork in the electrical socket";
        subtitles[3] = "Ow";
        

    }

    private void ResetString(SelectExitEventArgs arg0)
    {
        interactor = null;
        pullTabGrabObject.localPosition = Vector3.zero;
        stringRenderer.GetComponent<StringScript>().CreateString(null);
    }

    private void PrepareString(SelectEnterEventArgs arg0)
    {
        interactor = arg0.interactorObject;
    }

    private void Update()
    {
        if (interactor != null)
        {
            Vector3 grabPointLocalSpace = objectParent.InverseTransformPoint(pullTabGrabObject.position); // localPosition
            float grabPointMagnitude = grabPointLocalSpace.magnitude;

            if (grabPointMagnitude >= stringStretchLimit)
            {
                interactable.enabled = false;
                ResetString(null);
                interactable.enabled = true;
                if (audioClips[toypulled] != null)
                {
                    audioSource.PlayOneShot(audioClips[toypulled]);
                    textMesh.SetText(subtitles[toypulled]);
                    StartCoroutine(FadeCanvasSequence());
                }
                if (toypulled < 3) { toypulled++; }
            }
            stringRenderer.GetComponent<StringScript>().CreateString(pullTabGrabObject.position);
        }
    }
    
    private IEnumerator FadeCanvasSequence()
    {
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
