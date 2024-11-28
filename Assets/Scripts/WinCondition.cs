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
    private AudioSource audioSource;
    
    public ParticleSystem particles;

    private bool win = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
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
            audioSource.PlayOneShot(zap);
        }
        particles.Play();
        
        fade.FadeOut();
        yield return new WaitForSeconds(fade.fadeTime);
        
        closedDoor.gameObject.SetActive(false);
        openDoor.gameObject.SetActive(true);
        
        fade.FadeIn();
    }
}
