using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Start_Game : MonoBehaviour
{
    public string levelName;

    private void Start() {
        RenderSettings.skybox.SetColor("_Tint", new Color(0.4f,0.4f,0.4f,1f));
    }

    public void WaitThenLoad() {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel() {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(levelName);
    }
}
