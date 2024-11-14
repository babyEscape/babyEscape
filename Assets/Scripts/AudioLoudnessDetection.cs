using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLoudnessDetection : MonoBehaviour
{
    public int sampleWindow = 64;
    public bool isMicrophoneActive = true; // Toggle microphone detection
    private AudioClip microphoneClip;

    // Start is called before the first frame update
    void Start()
    {
        if (isMicrophoneActive)
        {
            MicrophoneToAudioClip();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isMicrophoneActive && !microphoneClip)
        {
            MicrophoneToAudioClip();
        }
        else if (!isMicrophoneActive && !microphoneClip)
        {
            StopMicrophone();
        }
    }

    public void MicrophoneToAudioClip()
    {
        // Get the first device from the input list
        if (Microphone.devices.Length > 0)
        {
            string microphoneName = Microphone.devices[0];
            microphoneClip = Microphone.Start(microphoneName, true, 20, AudioSettings.outputSampleRate);
        }
        else
        {
            Debug.LogWarning("No microphone detected!");
        }
    }

    public float GetLoudnessFromMicrophone()
    {
        if (!isMicrophoneActive || microphoneClip == null)
        {
            return 0;
        }

        return GetLoudnessFromAudioClip(Microphone.GetPosition(Microphone.devices[0]), microphoneClip);
    }

    public float GetLoudnessFromAudioClip(int clipPosition, AudioClip clip)
    {
        int startPosition = clipPosition - sampleWindow;

        if (startPosition < 0 || clip == null)
        {
            return 0;
        }

        float[] waveData = new float[sampleWindow];
        clip.GetData(waveData, startPosition);

        // Compute loudness
        float totalLoudness = 0;
        for (int i = 0; i < sampleWindow; i++)
        {
            totalLoudness += Mathf.Abs(waveData[i]);
        }

        return totalLoudness / sampleWindow;
    }

    public void StopMicrophone()
    {
        if (Microphone.IsRecording(null))
        {
            Microphone.End(null);
        }

        microphoneClip = null;
    }
}
