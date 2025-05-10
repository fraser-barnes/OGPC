using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.InputSystem;

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
            Debug.Log("pressed");
            if (button == 0) {
                Debug.Log("tutorial!");
                StartCoroutine(LoadLevel());
            }
            else if (button == 1)
                Debug.Log("settings!");
            else {
                Debug.Log("quit!");
                Application.Quit();
            }
        }
    }

    IEnumerator LoadLevel() {
        closeEyes.Play("CloseEyes");
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(5);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
