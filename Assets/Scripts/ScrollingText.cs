using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrollingText : MonoBehaviour
{
    [Header("Text Settings")]
    [TextArea][SerializeField] private string[] texts;
    [SerializeField] private float textSpeed;
    
    [Header("Scrolling Text")]
    [SerializeField] private TextMeshProUGUI textInfo;
    private int _currentDisplayingText = 0;

    public void ActivateText()
    {
        //Start Coroutine
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText()
    {
        for (int i = 0; i < texts[_currentDisplayingText].Length + 1; i++)
        {
            textInfo.text = texts[_currentDisplayingText].Substring(0, i);
            yield return new WaitForSeconds(textSpeed);
        }
    }

}
