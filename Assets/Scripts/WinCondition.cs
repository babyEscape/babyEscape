using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class WinCondition : MonoBehaviour
{
    public OVRScreenFade oVRScreenFade;
    public XRSocketInteractor currentSocket;

    public GameObject closedDoor;
    public GameObject openDoor;

    public float fadeTime;
    public OVRScreenFade fade;

    public AudioClip zap;
    public AudioClip doorOpen;
    private AudioSource audioSource;
    
    public ParticleSystem particles;

    private bool win = false;
    
    void Update()
    {
        if (currentSocket.hasSelection && win == false)
        {
            win = true;
            StartCoroutine(HandleWin());
        }
    }

    private IEnumerator HandleWin()
    {
        if (zap != null)
        {
            audioSource.PlayOneShot(doorOpen);
        }
        particles.Play();
        
        fade.FadeOut();
        yield return new WaitForSeconds(fade.fadeTime);
        
        if (doorOpen != null)
        {
            audioSource.PlayOneShot(doorOpen);
        }

        closedDoor.gameObject.SetActive(false);
        openDoor.gameObject.SetActive(true);
        
        fade.FadeIn();
    }
}
