using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MicroHoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    AudioClip recording;

    AudioSource audioSource;
    private float startRecordingTime;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        print("END_RECORDING");
        Microphone.End("");

        AudioClip recordingNew = AudioClip.Create(recording.name, (int)((Time.time - startRecordingTime) * recording.frequency), recording.channels, recording.frequency, false);
        float[] data = new float[(int)((Time.time - startRecordingTime) * recording.frequency)];
        recording.GetData(data, 0);
        recordingNew.SetData(data, 0);
        this.recording = recordingNew;

        audioSource.clip = recording;
        audioSource.Play();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        print("START_RECORDING");
        int minFreq;
        int maxFreq;
        int freq = 44100;
        Microphone.GetDeviceCaps("", out minFreq, out maxFreq);
        if (maxFreq < 44100)
            freq = maxFreq;

        recording = Microphone.Start("", false, 300, 44100);
        startRecordingTime = Time.time;
    }
}
