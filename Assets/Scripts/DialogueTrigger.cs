using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject player;
    public AudioClip bzzz;
    public AudioClip congrats;
    public GameObject electricityleft;
    public GameObject electricityright;
    
    public GameObject robot;
    public GameObject leftHand;
    public GameObject rightHand;
    private AudioSource robotSource;
    private AudioSource leftHandSource;
    private AudioSource rightHandSource;
    
    private bool triggered = false;

    public GameObject robotLeft;
    public GameObject robotRight;
    
    public GameObject canvasObject;
    public float uiDuration = 4f;
    public float uiStartDuration = 0.1f;
    private String subtitle1 = "Congratulations!";
    private String subtitle2 = "You now have electrical powers.";
    
    private CanvasGroup canvasGroup; 
    private TMP_Text textMesh;

    private void Awake()
    {
        canvasGroup = canvasObject.GetComponent<CanvasGroup>();
        textMesh = canvasGroup.GetComponentInChildren<TMP_Text>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        robotSource = robot.GetComponent<AudioSource>();
        leftHandSource = leftHand.GetComponent<AudioSource>();
        rightHandSource = rightHand.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject == player && triggered == false)
        {
            triggered = true;
            if (congrats != null)
            {
                robotSource.PlayOneShot(congrats);
                textMesh.SetText(subtitle1);
                Invoke("StartElectricity", 2f);
                StartCoroutine(FadeCanvasSequence());
            }
            
        }
    }

    private void StartElectricity()
    {
        if (bzzz != null)
        {
            rightHandSource.loop = true;
            leftHandSource.loop = true;
            rightHandSource.PlayOneShot(bzzz);
            leftHandSource.PlayOneShot(bzzz);
        }
        textMesh.SetText(subtitle2);
        electricityleft.SetActive(true);
        electricityright.SetActive(true);
        robotLeft.SetActive(true);
        robotRight.SetActive(true);
    }
    
    private IEnumerator FadeCanvasSequence()
    {
        yield return new WaitForSeconds(uiStartDuration);
    
        // Fade in
        yield return StartCoroutine(FadeCanvasGroup(canvasGroup, 0f, 1f, 1));
        
        // Wait for UI display time
        yield return new WaitForSeconds(uiDuration);
        
        // Fade out
        yield return StartCoroutine(FadeCanvasGroup(canvasGroup, 1f, 0f, 1));
    }
    
    private IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float start, float end, float duration)
    {
        float elapsedTime = 0f;
    
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, elapsedTime / duration);
            yield return null;
        }
    
        canvasGroup.alpha = end;
    }
}
