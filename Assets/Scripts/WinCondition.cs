using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public OVRScreenFade oVRScreenFade;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fork")) 
        {
            OnGameWin();
        }
    }

    private void OnGameWin()
    {
        if (oVRScreenFade != null)
        {
            oVRScreenFade.FadeOut();
        }
    }
    
}
