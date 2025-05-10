using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.InputSystem;
using TMPro;

public class Start_Game : MonoBehaviour
{
    public string levelName;

    private int button = 0;
    InputAction moveAction;
    InputAction confirmAction;
    bool moving = false;
    float movingHowLong = 0f;

    public RectTransform buttonLogo;
    public Animator closeEyes;
    bool settingsOpen = false;

    public GameObject title;
    public GameObject settingsMenu;
    private TMP_Text volume;
    private TMP_Text subtitles;

    public Settings settings;

    private void Start() {
        RenderSettings.skybox.SetColor("_Tint", new Color(0.4f,0.4f,0.4f,1f));
        moveAction = InputSystem.actions.FindAction("Move");
        confirmAction = InputSystem.actions.FindAction("Submit");
    }

    private void Update() {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        if (moveValue.y != 0)
            movingHowLong += Time.deltaTime;
        if (moveValue.y > 0 && !moving) {
            button--;
            moving = true;
        }
        else if (moveValue.y < 0 && !moving) {
            button++;
            moving = true;
        }
        else if (moveValue.y == 0 || movingHowLong > 0.3f) {
            moving = false;
            movingHowLong = 0f;
        }
        button = (int)Mathf.Clamp(button, 0, 2);
        buttonLogo.anchoredPosition = new Vector2(100f, button * -55);
        if (confirmAction.WasPressedThisFrame()) {
            if (settingsOpen)
            {
                if (button == 0 && moveValue.x >= 0)
                {
                    settings.volume += Time.deltaTime;
                    settings.volume = Mathf.Clamp(settings.volume, 0f, 100f);
                    volume.text = "Volume: " + settings.volume;
                } else if (button == 0 && moveValue.x < 0)
                {
                    settings.volume += Time.deltaTime;
                    settings.volume = Mathf.Clamp(settings.volume, 0f, 100f);
                    volume.text = "Volume: " + settings.volume;
                }
                else if (button == 1)
                {
                    settings.subtitles = !settings.subtitles;
                    if (settings.subtitles)
                        subtitles.text = "Subtitles: On";
                    else subtitles.text = "Subtitles: Off";
                }
                else
                {
                    settingsOpen = false;
                    settingsMenu.SetActive(false);
                    title.SetActive(true);
                }
            }
            else
            {
                if (button == 0)
                    StartCoroutine(LoadLevel());
                else if (button == 1)
                {
                    settingsOpen = true;
                    settingsMenu.SetActive(true);
                    title.SetActive(false);
                    volume = GameObject.Find("Volume").GetComponent<TMP_Text>();
                    subtitles = GameObject.Find("Settings").GetComponent<TMP_Text>();
                }
                else
                    Application.Quit();
            }
        }
    }

    IEnumerator LoadLevel() {
        closeEyes.Play("CloseEyes");
        GetComponentInChildren<AudioSource>().Play();
        yield return new WaitForSeconds(5);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
