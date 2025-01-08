using UnityEngine;

public class AI : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField]
    AudioClip[] clips;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        transform.localScale = new Vector3(1.01f, 0.01f, 1.01f);
        Talk(clips[0]);
    }

    void Talk(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
        float[] spectrum = new float[256];
        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.Hanning);
        foreach (float v in spectrum)
        {
            Debug.Log("volume: " + v);
        }

    }
}
