using UnityEngine;

public class AI : MonoBehaviour
{
    AudioSource audioSource;
    AudioClip[] clips;
    float[] spectrum = new float[128];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        transform.localScale = new Vector3(1.01f, 0.01f, 1.01f);
    }

    void Update()
    {
        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
        if (spectrum != null && spectrum.Length > 0)
        {
            float l = Mathf.Pow(spectrum[0] * 30,0.4f) + 0.01f;
            if (l <= 1f)
                transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1.01f, l, 1.01f), Time.deltaTime * 10f);
            else
                transform.localScale = new Vector3(1.01f, 1f, 1.01f);
        }
    }

    public void Talk(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
