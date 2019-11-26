using System.Collections;
using System.Collections.Generic;
//using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class AudioTinker : MonoBehaviour 
{
    public float frequency;
    public float sampleDurationSecs;
    private AudioSource audioSource;
    private AudioClip outAudioClip;


    public Slider frequencySlider;
    public Slider durationSlider;
    
    // Start is called before the first frame update
    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        frequency = frequencySlider.value;
        sampleDurationSecs = durationSlider.value;
        outAudioClip = CreateToneAudioClip(frequency);
    }

    /// <summary>
    /// Will save the sound effect currently being generated to the Unity assets folder with the name I give in the inspector.
    /// </summary>
    /// <param name="fileName"></param>
    public void SaveSound(string fileName)
    {
        Debug.Log("Saving" + fileName);
        SavWav.Save(fileName, outAudioClip);
    }

    // Public APIs
    public void PlayOutAudio() 
    {
        audioSource.PlayOneShot(outAudioClip);    
    }


    public void StopAudio() 
    {
        audioSource.Stop();
    }


    private AudioClip CreateToneAudioClip(float frequency) 
    {
        int sampleRate = 41000;
        int sampleLength = Mathf.FloorToInt(sampleRate * sampleDurationSecs);
        float maxValue = 1f / 4f;
        
        var audioClip = AudioClip.Create("tone", sampleLength, 1, sampleRate, false);
        
        float[] samples = new float[sampleLength];
        for (var i = 0; i < sampleLength; i++) {
            float s = Mathf.Sin(2.0f * Mathf.PI * frequency * ((float) i / (float) sampleRate));
            float v = s * maxValue;
            samples[i] = v;
        }

        audioClip.SetData(samples, 0);
        return audioClip;
    }

    
}