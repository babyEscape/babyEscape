using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCollider : MonoBehaviour
{
    public GameObject door;

    private int toysIn;

    public GameObject doorNewPosition;
    public AudioClip victorySound;
    public AudioClip doorSound;
    public OVRScreenFade fade;
    
    private AudioSource audioSource;
    private bool puzzleComplete = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        toysIn = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (toysIn >= 3 && !puzzleComplete)
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
        if (other.gameObject.tag == "toyBlock")
        {
            toysIn++;
        }
    }
}
