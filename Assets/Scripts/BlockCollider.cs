using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCollider : MonoBehaviour
{
    public GameObject door;
    public GameObject doorNewPosition;
    public GameObject cube;
    public GameObject cylinder;
    public GameObject triangle;
    public AudioClip victorySound;
    public AudioClip doorSound;
    public OVRScreenFade fade;
    
    private AudioSource audioSource;
    private bool cubeIn;
    private bool cylinderIn;
    private bool triangleIn;
    private bool puzzleComplete = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        cubeIn = false;
        cylinderIn = false;
        triangleIn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (cubeIn && cylinderIn && triangleIn && !puzzleComplete)
        {
            puzzleComplete = true;
            StartCoroutine(HandlePuzzleCompletion());
        }
    }

    private IEnumerator HandlePuzzleCompletion()
    {
        fade.FadeOut();
        yield return new WaitForSeconds(fade.fadeTime);

        if (victorySound != null)
        {
            audioSource.PlayOneShot(victorySound);
        }
        
        if (doorSound != null)
        {
            audioSource.PlayOneShot(doorSound);
        }

        door.gameObject.SetActive(false);
        doorNewPosition.gameObject.SetActive(true);
        
        fade.FadeIn();
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
        else if (other.gameObject == triangle)
        {
            triangleIn = true;
        }
    }
}
