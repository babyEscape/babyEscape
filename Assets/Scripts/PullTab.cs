using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class PullTab : MonoBehaviour
{
    public GameObject stringRenderer;
    public Transform pullTabGrabObject, objectParent;
    public float stringStretchLimit;
    public AudioClip toySound1, toySound2, toySound3, toySound4; 

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable interactable;
    private UnityEngine.XR.Interaction.Toolkit.Interactors.IXRSelectInteractor interactor;
    private AudioSource audioSource;
    private AudioClip[] audioClips = new AudioClip[4];
    private int toypulled = 0;

    private void Awake()
    {
        interactable = pullTabGrabObject.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
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
                }
                if (toypulled < 3) { toypulled++; }
            }
            stringRenderer.GetComponent<StringScript>().CreateString(pullTabGrabObject.position);
        }
    }
}
