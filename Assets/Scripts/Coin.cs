using UnityEngine;

public class Coin : MonoBehaviour {

    public enum CoinColor {
        BLACK, RED, BLUE, YELLOW
    }

    public enum CoinStatus {
        DEACTIVATED, TO_BE_PICKED, PICKED_UP_NOT_SAVED, PICKED_UP
    }

    public CoinColor color = CoinColor.BLACK;

    protected CoinStatus status = CoinStatus.DEACTIVATED;

    private SpriteRenderer spriteRenderer;
    private AudioSource collectSource;


    protected void Start() {

        spriteRenderer = GetComponent<SpriteRenderer>();
        status = CoinStatus.DEACTIVATED;

        Transform t = transform;
        while (collectSource == null) {
            t = t.parent;
            collectSource = t.GetComponent<AudioSource>();
        }
    }

    protected void Update() {
        spriteRenderer.enabled = status == CoinStatus.TO_BE_PICKED;
        foreach (Transform child in transform) {
            child.GetComponent<SpriteRenderer>().enabled = status == CoinStatus.TO_BE_PICKED;
        }
    }

    protected void OnTriggerEnter2D(Collider2D col) {
        if (status != CoinStatus.TO_BE_PICKED) {
            return;
        }

        if (col.gameObject.tag == "Player") {
            GameManager.Instance().AddCoinValue(color, 1);
            status = CoinStatus.PICKED_UP_NOT_SAVED;
            collectSource.Play();
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
