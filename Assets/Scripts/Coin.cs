using UnityEngine;

public class Coin : MonoBehaviour {

    public enum CoinColor {
        RED, BLUE, YELLOW, GREEN
    }

    public enum CoinStatus {
        PICKED_UP, PICKED_UP_NOT_SAVED, NOT_PICKED_UP
    }

    public Sprite spriteRed;
    public Sprite spriteBlue;
    public Sprite spriteYellow;
    public Sprite spriteGreen;

    public CoinColor color = CoinColor.RED;

    private SpriteRenderer spriteRenderer;
    private CoinStatus status;

    void Start() {

        spriteRenderer = GetComponent<SpriteRenderer>();

        status = CoinStatus.NOT_PICKED_UP;
        Sprite coinSprite = spriteRenderer.sprite;
        switch (color) {
            case Coin.CoinColor.RED:
                coinSprite = spriteRed;
                break;
            case Coin.CoinColor.BLUE:
                coinSprite = spriteBlue;
                break;
            case Coin.CoinColor.YELLOW:
                coinSprite = spriteYellow;
                break;
            case Coin.CoinColor.GREEN:
                coinSprite = spriteGreen;
                break;
            default:
                break;
        }
    }

    void Update() {
        spriteRenderer.enabled = status == CoinStatus.NOT_PICKED_UP;
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (status != CoinStatus.NOT_PICKED_UP) {
            return;
        }

        if (col.gameObject.tag == "Player") {
            GameManager.Instance().AddCoinValue(color, 1);
            status = CoinStatus.PICKED_UP_NOT_SAVED;
        }
    }

    public CoinStatus GetStatus() {

        return status;
    }

    public void saveCoin() {
        status = CoinStatus.PICKED_UP;
    }

    public void revertPickUp() {
        status = CoinStatus.NOT_PICKED_UP;
        GameManager.Instance().AddCoinValue(color, -1);
    }

}
