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

    void Update()
    {
        if (currentSocket.hasSelection)
        {
            oVRScreenFade.FadeOut();
        }
    }
    
}
