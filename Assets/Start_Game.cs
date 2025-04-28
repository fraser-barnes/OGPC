using UnityEngine;
using UnityEngine.SceneManagement;
public class Start_Game : MonoBehaviour
{
    public string levelName;

    public void LoadLevel(){
        if (levelName.equals("Quit")){
            Application.Quit();
        }
        else{
            SceneManager.LoadScene(levelName);
        }
    }
}
