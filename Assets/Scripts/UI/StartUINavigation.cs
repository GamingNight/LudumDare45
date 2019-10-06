using UnityEngine;
using UnityEngine.UI;

public class StartUINavigation : MonoBehaviour {

    public Image cursor;
    public Text start;
    public Text quit;

    public Font OpenLight;
    public Font OpenBold;

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
                currentCursorValue =1;
                start.font = OpenLight;
                quit.font = OpenBold;
            } else if (currentCursorValue > 1) {
                currentCursorValue = 0;
                start.font = OpenBold;
                quit.font = OpenLight;
            }

        }
        prevVerticalvalue = v;

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) {
            switch (currentCursorValue) {
                case 0:
                    StartWithNothing();
                    break;
                case 1:
                    Quit();
                    break;
                default:
                    break;
            }
        }
    }

    public void StartWithNothing() {

        gameObject.SetActive(false);
        gameContainer.SetActive(true);

    }

    public void Quit() {
        Application.Quit();
    }
}
