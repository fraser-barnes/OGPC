using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{   
    public static bool gamePaused = false;
    public GameObject pauseMenu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)){
            if (gamePaused){
                Resume();
            }
            else{
                Pause();
            }
        }
    }

    void Resume(){
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }

    void Pause(){
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }
}
