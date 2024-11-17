using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorOnCollision : MonoBehaviour
{
    private Renderer cubeRenderer;

    void Start()
    {
        // Get the Renderer component to change the color later
        cubeRenderer = GetComponent<Renderer>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object is the sphere
        if (collision.gameObject.CompareTag("Dummy"))
        {
            // Change the cube's color to a random color
            cubeRenderer.material.color = new Color(
                Random.Range(0f, 1f), 
                Random.Range(0f, 1f), 
                Random.Range(0f, 1f)
            );
        }
    }
}

