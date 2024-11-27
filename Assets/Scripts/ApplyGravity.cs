using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Climbing;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement;

public class ApplyGravity : MonoBehaviour
{
    [SerializeField] private GrabMoveProvider grabMoveProviderLeft;
    [SerializeField] private GrabMoveProvider grabMoveProviderRight;

    private bool _isClimbing = false;

    public ClimbInteractable[] climbInteractables;
    // Update is called once per frame

    private void Awake()
    {
        climbInteractables = FindObjectsOfType<ClimbInteractable>();
    }

    void Update()
    {

        _isClimbing = CheckIfClimbing();

        if(_isClimbing == false) 
        {
            grabMoveProviderLeft.enabled = true;
            grabMoveProviderRight.enabled = true;
        }
        else
        {
            grabMoveProviderLeft.enabled = false;
            grabMoveProviderRight.enabled = false;
        }
    }

    private bool CheckIfClimbing()
    {
        foreach (ClimbInteractable climbInteractable in climbInteractables)
        {
            if (climbInteractable.isSelected)
            {
                // Debug.Log("Currently Climbing: Gravity Disabled");
                return true;
            }
        }
        
        // Debug.Log("No Climbing: Gravity Enabled"); 
        return false;
    }


}
