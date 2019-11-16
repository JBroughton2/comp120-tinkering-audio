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

    /*
    private void IncreaseVolume()
    {
        float[] samples = new float[audioSource.clip.samples * audioSource.clip.channels];
        audioSource.clip.GetData(samples, 0);

        for (int i = 0; i < samples.Length; i++)
        {
            samples[i] = samples[i] * 0.5f;
        }

        audioSource.clip.SetData(samples, 0);
    }
    */
    
#if UNITY_EDITOR
//    [Button("Save Wav file")]
    private void SaveWavFile() {
        string path = EditorUtility.SaveFilePanel("Where do you want the wav file to go?", "", "", "wav");
        var audioClip = CreateToneAudioClip(1500);
        SaveWavUtil.Save(path, audioClip);
    }
#endif
}