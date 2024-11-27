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

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable interactable;
    private UnityEngine.XR.Interaction.Toolkit.Interactors.IXRSelectInteractor interactor;

    private void Awake()
    {
        interactable = pullTabGrabObject.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
    }

    private void Start()
    {
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
            }

            stringRenderer.GetComponent<StringScript>().CreateString(pullTabGrabObject.position);
        }
    }
}
