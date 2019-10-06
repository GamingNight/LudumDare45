using UnityEngine;

public class Coin : MonoBehaviour {

    public enum CoinColor {
        RED, BLUE, YELLOW, GREEN
    }

    public enum CoinStatus {
        DEACTIVATED, TO_BE_PICKED, PICKED_UP_NOT_SAVED, PICKED_UP
    }

    public Sprite spriteRed;
    public Sprite spriteBlue;
    public Sprite spriteYellow;
    public Sprite spriteGreen;

    public CoinColor color = CoinColor.RED;

    private SpriteRenderer spriteRenderer;
    protected CoinStatus status = CoinStatus.DEACTIVATED;

    protected void Start() {

        spriteRenderer = GetComponent<SpriteRenderer>();

        status = CoinStatus.DEACTIVATED;
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

    protected void Update() {
        spriteRenderer.enabled = status == CoinStatus.TO_BE_PICKED;
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (status != CoinStatus.TO_BE_PICKED) {
            return;
        }

        if (col.gameObject.tag == "Player") {
            GameManager.Instance().AddCoinValue(color, 1);
            status = CoinStatus.PICKED_UP_NOT_SAVED;
        }
    }

    public void SaveCoin() {
        if (status == CoinStatus.PICKED_UP_NOT_SAVED) {
            status = CoinStatus.PICKED_UP;
        }
    }

    public void ResetPickUp() {
        if (status == CoinStatus.PICKED_UP_NOT_SAVED) {
            GameManager.Instance().AddCoinValue(color, -1);
            status = CoinStatus.TO_BE_PICKED;
        }
    }

    public void Activate() {
        if (status == CoinStatus.DEACTIVATED) {
            status = CoinStatus.TO_BE_PICKED;
        }
    }

    public void Deactivate() {
        status = CoinStatus.DEACTIVATED;
    }

    public Coin.CoinStatus GetStatus() {
        return status;
    }
}
