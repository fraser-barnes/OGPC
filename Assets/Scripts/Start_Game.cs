using UnityEngine;
using UnityEngine.SceneManagement;
public class Start_Game : MonoBehaviour
{
    public string levelName;

    public void WaitThenLoad() {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel(){
        SceneManager.LoadScene(levelName);
    }
}
