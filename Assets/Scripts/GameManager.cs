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

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    public void UpdateCoinCount(Coin.CoinColor color) {

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
}
