using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject player;
    public AudioClip bzzz;
    public AudioClip congrats;
    public GameObject electricityleft;
    public GameObject electricityright;
    
    public GameObject robot;
    public GameObject leftHand;
    public GameObject rightHand;
    private AudioSource robotSource;
    private AudioSource leftHandSource;
    private AudioSource rightHandSource;
    
    private bool triggered = false;

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
        if (other.gameObject == player && triggered == false)
        {
            triggered = true;
            if (congrats != null)
            {
                robotSource.PlayOneShot(congrats);
                Invoke("StartElectricity", 2f);
            }
            
        }
    }

    private void StartElectricity()
    {
        if (bzzz != null)
        {
            rightHandSource.loop = true;
            leftHandSource.loop = true;
            rightHandSource.PlayOneShot(bzzz);
            leftHandSource.PlayOneShot(bzzz);
        }
        electricityleft.SetActive(true);
        electricityright.SetActive(true);
    }
}
