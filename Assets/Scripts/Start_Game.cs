using UnityEngine;
using UnityEngine.SceneManagement;
public class Start_Game : MonoBehaviour
{
    public string levelName;

    public void LoadLevel(){
        SceneManager.LoadScene(levelName);
    }
}
