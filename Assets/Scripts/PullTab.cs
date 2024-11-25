using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;


public class PullTab : MonoBehaviour
{
    public GameObject stringRenderer;
    public Transform pullTabGrabObject;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable interactable;
    private Transform interactor;

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
        interactor = arg0.interactorObject.transform;
    }

    private void Update()
    {
        if (interactor != null)
        {
            stringRenderer.GetComponent<StringScript>().CreateString(pullTabGrabObject.position);
        }
    }
}
