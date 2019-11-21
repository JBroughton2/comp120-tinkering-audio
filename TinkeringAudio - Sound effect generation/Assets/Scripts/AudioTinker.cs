using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class AudioTinker : MonoBehaviour
{
    public int frequency;
    public int sampleDur;
    private AudioSource audioSource;
    private AudioClip outAudioClip;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
      
    }

    public void PlayOutAudio(int freq, float dur)
    {
        outAudioClip = CreateToneAudioClip(freq, dur);
        audioSource.PlayOneShot(outAudioClip);     
    }


    public void StopAudio()
    {
        audioSource.Stop();
    }


    public AudioClip CreateToneAudioClip(int frequency, float sampleDur)
    {
        float sampleDurationSecs = sampleDur;
        int sampleRate = 70000;
        int sampleLength = Mathf.FloorToInt(sampleRate * sampleDur);
        float maxValue = 3f / 10f;

        var audioClip = AudioClip.Create("tone", sampleLength, 1, sampleRate, false);

        float[] samples = new float[sampleLength];
        for (var i = 0; i < sampleLength; i++)
        {
            float s = Mathf.Sin(2.0f * Mathf.PI * frequency * ((float)i / (float)sampleRate));
            float v = s * maxValue;
            samples[i] = v;
        }

        audioClip.SetData(samples, 0);
        return audioClip;
    }



}