using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject player;
    public AudioClip bzzz;
    public AudioClip congrats;
    public ParticleSystem electricityleft;
    public ParticleSystem electricityright;
    
    public GameObject robot;
    public GameObject leftHand;
    public GameObject rightHand;
    private AudioSource robotSource;
    private AudioSource leftHandSource;
    private AudioSource rightHandSource;

    // Start is called before the first frame update
    void Start()
    {
        robotSource = robot.GetComponent<AudioSource>();
        leftHandSource = leftHand.GetComponent<AudioSource>();
        rightHandSource = rightHand.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject == player)
        {
            if (congrats != null && bzzz != null)
            {
                robotSource.PlayOneShot(congrats);
                rightHandSource.loop = true;
                leftHandSource.loop = true;
                rightHandSource.PlayOneShot(bzzz);
                leftHandSource.PlayOneShot(bzzz);
            }
            electricityleft.Play();
            electricityright.Play();
        }
    }
}
