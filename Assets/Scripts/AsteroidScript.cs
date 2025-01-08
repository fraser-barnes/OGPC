using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    private float lifetime;
    void Update()
    {
        lifetime += Time.deltaTime;
        if (lifetime > 5)
        {
            lifetime = 0;
            gameObject.SetActive(false);
            
        }
    }
}
