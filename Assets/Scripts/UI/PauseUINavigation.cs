using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PauseUINavigation : MonoBehaviour {

    public GameObject pauseCanvas;
    public Text resume;
    public Text restart;
    public Text quit;

    public Font lightFont;
    public Font boldFont;

    public AudioSource menuSelectSound;

    private int currentCursorValue;
    private float prevVerticalvalue;
    private AudioSource menuHoverSound;
    private PauseManager pauseManager;

    void Start() {
        currentCursorValue = 0;
        menuHoverSound = GetComponent<AudioSource>();
        pauseManager = GetComponent<PauseManager>();
    }

    void OnEnable() {
        currentCursorValue = 0;
    }

    void Update() {
        float v = Input.GetAxisRaw("Vertical");

        if (v != 0 && v != prevVerticalvalue) {
            int newCursorValue = currentCursorValue - (int)v;
            if (newCursorValue < 0) {
                newCursorValue = 2;
            } else if (newCursorValue > 2) {
                newCursorValue = 0;
            }
            FocusCursorOnvalue(newCursorValue);
        }

        prevVerticalvalue = v;

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) {
            switch (currentCursorValue) {
                case 0:
                    Resume();
                    break;
                case 1:
                    RestartWithNothing();
                    break;
                case 2:
                    Quit();
                    break;
                default:
                    break;
            }
        }
    }

    public void FocusCursorOnvalue(int value) {
        switch (value) {
            case 0:
                resume.font = boldFont;
                restart.font = lightFont;
                quit.font = lightFont;
                break;
            case 1:
                resume.font = lightFont;
                restart.font = boldFont;
                quit.font = lightFont;
                break;
            case 2:
                resume.font = lightFont;
                restart.font = lightFont;
                quit.font = boldFont;
                break;
            default:
                break;
        }
        if (currentCursorValue != value) {
            menuHoverSound.Play();
        }
        currentCursorValue = value;
    }

    public void Resume() {

        menuSelectSound.Play();
        pauseManager.ResumeGame();
    }

    public void RestartWithNothing() {

        menuSelectSound.Play();
        pauseCanvas.SetActive(false);
        GameManager.Instance().ResetGame();
        pauseManager.ResumeGame();
    }

    public void Quit() {
        Application.Quit();
    }
}
