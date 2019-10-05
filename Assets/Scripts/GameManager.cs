using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager instance;

    public static GameManager Instance() {

        return instance;
    }

    private int redCoinCount;
    private int blueCoinCount;
    private int greenCoinCount;
    private int yellowCoinCount;
    public GameObject platformContainer;
    public GameObject peakContainer;
    public GameObject mobilePeakContainer;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    void Start() {

        redCoinCount = 0;
        blueCoinCount = 0;
        yellowCoinCount = 0;
        greenCoinCount = 0;
    }

    public void AddCoinValue(Coin.CoinColor color, int val) {

        switch (color) {
            case Coin.CoinColor.RED:
                redCoinCount += val;
                break;
            case Coin.CoinColor.BLUE:
                blueCoinCount += val;
                break;
            case Coin.CoinColor.YELLOW:
                yellowCoinCount += val;
                break;
            case Coin.CoinColor.GREEN:
                greenCoinCount += val;
                break;
            default:
                break;
        }
    }

    void Update() {

        SetPlatformActivation(redCoinCount > 0);
        SetPeakActivation(blueCoinCount > 0);
        SetMobilePeakActivation(greenCoinCount > 0);
    }

    private void SetPlatformActivation(bool value) {

        foreach (Transform platform in platformContainer.transform) {
            platform.gameObject.SetActive(value);
        }
    }

    private void SetPeakActivation(bool value) {

        foreach (Transform peak in peakContainer.transform) {
            peak.gameObject.SetActive(value);
        }
    }

    private void SetMobilePeakActivation(bool value) {

        foreach (Transform mPeak in mobilePeakContainer.transform) {
            mPeak.gameObject.SetActive(value);
        }
    }
}
