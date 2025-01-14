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
        for (int i = 0; i < circles.Length; i++)
        {
            circles[i].localPosition = Vector3.zero;
        }
    }

    void Update()
    {
        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
        for (int i = 0; i < circles.Length; i++)
        {
            if (spectrum != null && spectrum.Length > 0)
            {
                float l = Mathf.Pow(spectrum[(i + 1) * 5] * 400,0.25f) + 1.01f;
                if (l <= 3f)
                    circles[i].localScale = Vector3.Lerp(circles[i].localScale, new Vector3(l, 0.015f, l), Time.deltaTime * 30f);
                else
                    circles[i].localScale = new Vector3(1.01f, 0.015f, 1.01f);
            }
            if (audioSource.isPlaying)
                circles[i].localPosition = Vector3.Lerp(circles[i].localPosition, new Vector3(0f, (i-10)/10f, 0f), Time.deltaTime * 20f);
            else
                circles[i].localPosition = Vector3.Lerp(circles[i].localPosition, Vector3.zero, Time.deltaTime * 10f);
        }

    }

    public void Talk(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
