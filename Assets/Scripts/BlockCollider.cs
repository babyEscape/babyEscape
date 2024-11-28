using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCollider : MonoBehaviour
{
    public GameObject door;

    private int toysIn;


    // Start is called before the first frame update
    void Start()
    {
        toysIn = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (toysIn >= 3)
        { 
            door.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "toyBlock")
        {
            toysIn++;
        }
    }
}
