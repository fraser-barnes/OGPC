using UnityEngine;
using UnityEngine.SceneManagement;

public class endingScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
   {

        Debug.Log("collided");
       // Check if the collision is with an asteroid
       if (collision.gameObject.CompareTag("Player"))
       {
            Debug.Log("loading scene");
             SceneManager.LoadScene("Level2");

       }
   }
}
