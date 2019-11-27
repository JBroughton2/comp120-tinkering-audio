using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class AudioTinker : MonoBehaviour
{
    // Inputted variables for adjusting the sound
    public int frequency;
    public int sampleDur;
    public int sampRate;

    // References for the unity audio source and clip
    private AudioSource audioSource;
    private AudioClip outAudioClip;


    // Start is called before the first frame update
    void Start()
    {
        // A method to grab the audiosource component on the game object
        audioSource = GetComponent<AudioSource>();      
    }

    // A public function that holds the variables for frequency and duration
    public void PlayOutAudio(int freq, float dur, int rate)
    {
        outAudioClip = CreateToneAudioClip(freq, dur, rate);
        audioSource.PlayOneShot(outAudioClip);     
    }

    // A function to stop the audio
    private void StopAudio()
    {
        audioSource.Stop();
    }

    // Here is the function where the sound is created
    private AudioClip CreateToneAudioClip(int frequency, float sampleDur, int sampRate)
    {
        float sampleDurationSecs = sampleDur;
       // int sampleRate = 70000;

        // As I needed to create a float for the duration to make the sounds shorter I converted the float variable into an int
        int sampleLength = Mathf.FloorToInt(sampRate * sampleDur);
        float maxValue = 3f / 10f;

        var audioClip = AudioClip.Create("tone", sampleLength, 1, sampRate, false);

        float[] samples = new float[sampleLength];
        for (var i = 0; i < sampleLength; i++)
        {
            float s = Mathf.Sin(2.0f * Mathf.PI * frequency * ((float)i / (float)sampRate));
            float v = s * maxValue;
            samples[i] = v;
        }

        audioClip.SetData(samples, 0);
        return audioClip;
    }



}