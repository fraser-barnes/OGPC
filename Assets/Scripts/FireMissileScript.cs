using UnityEngine;

private int fireCooldown = 5;
private int timeSinceFired = 6;
public GameObject firePoint; 

public class FireMissileScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        timeSinceFired += Time.deltaTime;

        if (Input.GetKey(KeyCode.z) && timeSinceFired > fireCooldown){
            SetActive(true);
            transform.position = firePoint.position;
        }
    }
}
