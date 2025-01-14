using UnityEngine;

public class AI : MonoBehaviour
{
    AudioSource audioSource;
    AudioClip[] clips;
    float[] spectrum = new float[128];
    [SerializeField]
    Transform[] circles;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
        for (int i = 0; i < circles.Length; i++)
        {
            if (spectrum != null && spectrum.Length > 0)
            {
                float l = Mathf.Pow(spectrum[(i + 1) * 10] * 500,0.2f) + 0.5f;
                if (l <= 5f)
                    circles[i].localScale = Vector3.Lerp(circles[i].localScale, new Vector3(0.5f, l, 1f), Time.deltaTime * 30f);
                else
                    circles[i].localScale = new Vector3(0.5f, 0.5f, 1f);
            }
        }

    }

    public void Talk(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
