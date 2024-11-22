using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCollider : MonoBehaviour
{
    public GameObject door;
    public GameObject cube;
    public GameObject cylinder;
    public GameObject triangle;

    private bool cubeIn;
    private bool cylinderIn;
    private bool triangleIn;


    // Start is called before the first frame update
    void Start()
    {
        cubeIn = false;
        cylinderIn = false;
        triangleIn = true; // set this to false later when we have a triangle model
    }

    // Update is called once per frame
    void Update()
    {
        if (cubeIn && cylinderIn && triangleIn)
        { 
            door.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject == cube)
        {
            cubeIn = true;
        }
        else if (other.gameObject == cylinder)
        {
            cylinderIn = true;
        }
        // else if (other.gameObject == triangle)
        // {
        //     triangleIn = true;
        // }
    }
}
