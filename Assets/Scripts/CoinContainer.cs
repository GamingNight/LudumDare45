using UnityEngine;

public class CoinContainer : MonoBehaviour {


    public GameObject coinsContainerObject;

    private Coin.CoinStatus status = Coin.CoinStatus.TO_BE_PICKED;

    void Start() {
        status = Coin.CoinStatus.TO_BE_PICKED;
    }

    void Update() {
        SetCoinsActivation((status != Coin.CoinStatus.TO_BE_PICKED));
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (status != Coin.CoinStatus.TO_BE_PICKED) {
            return;
        }

        if (col.gameObject.tag == "Player") {
            status = Coin.CoinStatus.PICKED_UP_NOT_SAVED;
        }
    }

    public Coin.CoinStatus GetStatus() {
        return status;
    }

    public void SaveCoin() {
        status = Coin.CoinStatus.PICKED_UP;
    }

    public void ResetPickUp() {
        status = Coin.CoinStatus.TO_BE_PICKED;
    }


    private void SetCoinsActivation(bool value) {
        foreach (Transform coin in coinsContainerObject.transform) {
            coin.gameObject.SetActive(value);
        }
    }

}
