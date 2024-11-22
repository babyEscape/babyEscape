using System.Collections;
using System.Collections.Generic;
using Unity.VRTemplate;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class ShootFromMicrophone : MonoBehaviour
{
    public AudioLoudnessDetection detector;

    public float loudnessSensibility = 100;
    public float threshold = 0.1f;
    
    public Transform _startPoint = null;
    public float _launchSpeed = 10.0f;
    
    public XRSocketInteractor currentSocket;
    
    public float respawnDistance = 10f; // Distance threshold for respawn
    public float timeToRespawn = 5.0f;

    private Rigidbody rb;
    private bool isLaunched = false;
    private float timeSinceLaunch = 0f;

    public GameObject dummyModel = null;
    
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
                Invoke("ActivateSocket", 0.5f);
            }
        }
        
        if (isLaunched)
        {
            float distance = Vector3.Distance(transform.position, _startPoint.position);
            if (distance > respawnDistance || (Time.timeSinceLevelLoad - timeSinceLaunch) > timeToRespawn)
            {
                RespawnDummy();
                HideDummy();
                Invoke("ShowDummy", 0.3f);
            }
        }
    }
    
    void ApplyForce()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(_startPoint.forward * _launchSpeed, ForceMode.Impulse);
        isLaunched = true;
        timeSinceLaunch = Time.timeSinceLevelLoad;
    }

    void ActivateSocket()
    {
        currentSocket.socketActive = true;
    }
    
    void RespawnDummy()
    {
        isLaunched = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = currentSocket.transform.position;
        transform.rotation = currentSocket.transform.rotation;
        currentSocket.StartManualInteraction(GetComponent<IXRSelectInteractable>());
    }

    void HideDummy()
    {
        dummyModel.transform.localScale = Vector3.zero;
    }

    void ShowDummy()
    {
        dummyModel.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
    }
}
