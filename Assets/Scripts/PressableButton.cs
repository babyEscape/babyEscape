using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressableButton : MonoBehaviour
{
    public Color pressedColor = Color.green; // Color when pressed
    public Color defaultColor = Color.red;  // Default color
    private Material buttonMaterial;

    void Start()
    {
        // Get the Renderer component to change the button's material
        Renderer buttonRenderer = GetComponent<Renderer>();
        
        // Clone the material to avoid modifying the shared material
        buttonMaterial = buttonRenderer.material;
        
        // Set the default color
        buttonMaterial.color = defaultColor;
    }

    // Detect collision with a kinematic object
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collision Detected with: {collision.gameObject.name}");

        if (collision.rigidbody != null && collision.rigidbody.isKinematic)
        {
            Debug.Log("Changing color to pressedColor");
            buttonMaterial.color = pressedColor; // Change to pressed color
        }
    }

    // Reset color when the object exits
    void OnCollisionExit(Collision collision)
    {
        Debug.Log($"Collision Ended with: {collision.gameObject.name}");

        if (collision.rigidbody != null && collision.rigidbody.isKinematic)
        {
            Debug.Log("Resetting color to defaultColor");
            buttonMaterial.color = defaultColor; // Reset to default color
        }
    }
}