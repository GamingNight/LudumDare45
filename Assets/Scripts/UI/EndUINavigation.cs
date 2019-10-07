using UnityEngine;
using UnityEngine.UI;

public class EndUINavigation : MonoBehaviour {

    public Text blackCoins;
    public Text redCoins;
    public Text blueCoins;
    public Text yellowCoins;
    public Text restart;
    public Text quit;
    public Font lightFont;
    public Font boldFont;

    public GameObject gameContainer;

    private int currentCursorValue;
    private float prevVerticalvalue;

    void Start() {
        currentCursorValue = 0;
    }

    void OnEnable() {
        blackCoins.text = "Black " + GameManager.Instance().GetCoinCount(Coin.CoinColor.BLACK) + "/20";
        redCoins.text = "Red " + GameManager.Instance().GetCoinCount(Coin.CoinColor.RED) + "/20";
        blueCoins.text = "Blue " + GameManager.Instance().GetCoinCount(Coin.CoinColor.BLUE) + "/20";
        yellowCoins.text = "Yellow " + GameManager.Instance().GetCoinCount(Coin.CoinColor.YELLOW) + "/20";
    }

    void Update() {
        float v = Input.GetAxisRaw("Vertical");

        if (v != 0 && v != prevVerticalvalue) {
            int newCursorValue = currentCursorValue - (int)v;
            if (newCursorValue < 0) {
                newCursorValue = 1;
            } else if (newCursorValue > 1) {
                newCursorValue = 0;
            }
            FocusCursorOnvalue(newCursorValue);
        }

        prevVerticalvalue = v;

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) {
            switch (currentCursorValue) {
                case 0:
                    RestartWithNothing();
                    break;
                case 1:
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
                restart.font = boldFont;
                quit.font = lightFont;
                break;
            case 1:
                restart.font = lightFont;
                quit.font = boldFont;
                break;
            default:
                break;
        }
        currentCursorValue = value;
    }

    public void RestartWithNothing() {

        gameObject.SetActive(false);
        GameManager.Instance().ResetGame();
        gameContainer.SetActive(true);
    }

    public void Quit() {
        Application.Quit();
    }
}
