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

    public void AddCoin(Coin.CoinColor color) {

        switch (color) {
            case Coin.CoinColor.RED:
                redCoinCount++;
                break;
            case Coin.CoinColor.BLUE:
                blueCoinCount++;
                break;
            case Coin.CoinColor.YELLOW:
                yellowCoinCount++;
                break;
            case Coin.CoinColor.GREEN:
                greenCoinCount++;
                break;
            default:
                break;
        }
    }

    void Update() {

        if (redCoinCount == 1) {
            RevealPlatforms();
        }
        if (blueCoinCount == 1) {
            RevealPeaks();
        }
        if (greenCoinCount == 1) {
            RevealMobilePeaks();
        }
    }

    private void RevealPlatforms() {

        foreach (Transform platform in platformContainer.transform) {
            platform.gameObject.SetActive(true);
        }
    }

    private void RevealPeaks() {

        foreach (Transform peak in peakContainer.transform) {
            peak.gameObject.SetActive(true);
        }
    }

    private void RevealMobilePeaks() {

        foreach (Transform mPeak in mobilePeakContainer.transform) {
            mPeak.gameObject.SetActive(true);
        }
    }
}
