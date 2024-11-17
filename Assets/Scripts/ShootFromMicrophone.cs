using System.Collections;
using System.Collections.Generic;
using Unity.VRTemplate;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class ShootFromMicrophone : MonoBehaviour
{
    public AudioSource source;
    public Vector3 minScale;
    public Vector3 maxScale;
    public AudioLoudnessDetection detector;

    public float loudnessSensibility = 100;
    public float threshold = 0.1f;
    
    public Transform _startPoint = null;
    public float _launchSpeed = 10.0f;
    
    public XRSocketInteractor currentSocket;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentSocket.hasSelection)
        {
            float loudness = detector.GetLoudnessFromMicrophone() * loudnessSensibility;
            if (loudness > threshold)
            {
                Debug.Log("fire!");
                currentSocket.socketActive = false;
                Invoke("ApplyForce", 0.05f);
                Invoke("ActivateSocket", 2.0f);
            }
        }
    }
    
    void ApplyForce()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(_startPoint.forward * _launchSpeed, ForceMode.Impulse);
    }

    void ActivateSocket()
    {
        currentSocket.socketActive = true;
    }
    
}
