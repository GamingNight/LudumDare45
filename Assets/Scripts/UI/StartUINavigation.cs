using UnityEngine;
using UnityEngine.UI;

public class StartUINavigation : MonoBehaviour {

    public Image cursor;
    public Text start;
    public Text options;
    public Text quit;

    public GameObject gameContainer;

    private int currentCursorValue;
    private float prevVerticalvalue;
    void Start() {
        currentCursorValue = 0;
    }

    void Update() {

        float v = Input.GetAxisRaw("Vertical");

        if (v != 0 && v != prevVerticalvalue) {
            currentCursorValue -= (int)v;
            if (currentCursorValue < 0) {
                currentCursorValue = 2;
            } else if (currentCursorValue > 2) {
                currentCursorValue = 0;
            }

            FocusCursorOnvalue(currentCursorValue);
        }
        prevVerticalvalue = v;

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) {
            switch (currentCursorValue) {
                case 0:
                    StartWithNothing();
                    break;
                case 1:
                    ShowOptions();
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
                cursor.rectTransform.position = new Vector3(cursor.rectTransform.position.x, start.rectTransform.position.y, cursor.rectTransform.position.z);
                break;
            case 1:
                cursor.rectTransform.position = new Vector3(cursor.rectTransform.position.x, options.rectTransform.position.y, cursor.rectTransform.position.z);
                break;
            case 2:
                cursor.rectTransform.position = new Vector3(cursor.rectTransform.position.x, quit.rectTransform.position.y, cursor.rectTransform.position.z);
                break;
            default:
                break;
        }
    }

    public void StartWithNothing() {

        gameObject.SetActive(false);
        gameContainer.SetActive(true);

    }

    public void ShowOptions() {

        //TODO
    }

    public void Quit() {
        Application.Quit();
    }
}
