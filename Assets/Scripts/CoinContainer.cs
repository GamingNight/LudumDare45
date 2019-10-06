using UnityEngine;

public class CoinContainer : MonoBehaviour {


    public GameObject coinsContainerObject;

    public enum CoinContainerStatus {
        PICKED_UP, PICKED_UP_NOT_SAVED, NOT_PICKED_UP
    }
    private CoinContainerStatus status = CoinContainerStatus.NOT_PICKED_UP;

    void Start() {
        status = CoinContainerStatus.NOT_PICKED_UP;
    }

    void Update() {
        SetCoinsActivation((status != CoinContainerStatus.NOT_PICKED_UP));
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (status != CoinContainerStatus.NOT_PICKED_UP) {
            return;
        }

        if (col.gameObject.tag == "Player") {
            status = CoinContainerStatus.PICKED_UP_NOT_SAVED;
        }
    }

    public CoinContainerStatus GetStatus() {
        return status;
    }

    public void saveCoin() {
        status = CoinContainerStatus.PICKED_UP;
    }

    public void resetPickUp() {
        status = CoinContainerStatus.NOT_PICKED_UP;
    }


    private void SetCoinsActivation(bool value) {
        foreach (Transform coin in coinsContainerObject.transform) {
            coin.gameObject.SetActive(value);
        }
    }

}
