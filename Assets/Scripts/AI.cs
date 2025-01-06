using UnityEngine;

public class AI : MonoBehaviour
{
    AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        transform.localScale = new Vector3(1.01f, 0.01f, 1.01f);
    }

    void Talk(AudioClip clip)
    {
        audioSource.clip = clip;
        float[] spectrum = new float[256];
        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.Hanning);
        
    }
}
