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
    
    public Transform playerHead; // Assign the player's head transform
    public float respawnDistance = 10f; // Distance threshold for respawn

    private Rigidbody rb;
    private bool isLaunched = false;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        
        if (isLaunched)
        {
            float distance = Vector3.Distance(transform.position, playerHead.position);
            if (distance > respawnDistance)
            {
                RespawnDummy();
            }
        }
    }
    
    void ApplyForce()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(_startPoint.forward * _launchSpeed, ForceMode.Impulse);
        isLaunched = true;
    }

    void ActivateSocket()
    {
        currentSocket.socketActive = true;
    }
    
    void RespawnDummy()
    {
        isLaunched = false;
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = currentSocket.transform.position;
        transform.rotation = currentSocket.transform.rotation;
        currentSocket.StartManualInteraction(GetComponent<IXRSelectInteractable>());
    }
    
}
