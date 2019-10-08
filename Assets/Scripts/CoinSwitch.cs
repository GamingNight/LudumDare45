using UnityEngine;

public class CoinSwitch : Coin {
    public GameObject coinContainer;

    new void Start() {
        base.Start();
        status = CoinStatus.TO_BE_PICKED;
    }

    new void Update() {
        base.Update();
        if (status == CoinStatus.PICKED_UP_NOT_SAVED || status == CoinStatus.PICKED_UP) {
            ActivateSubCoins();
        } else {
            DeactivateSubCoins();
        }
    }

    private void ActivateSubCoins() {
        foreach (Transform child in coinContainer.transform) {
            child.GetComponent<Coin>().Activate();
        }
    }

    private void DeactivateSubCoins() {
        foreach (Transform child in coinContainer.transform) {
            child.GetComponent<Coin>().Deactivate();
        }
    }

    new void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player" && status == CoinStatus.TO_BE_PICKED && color == CoinColor.BLACK) {
            GameManager.Instance().musicSource.Play();
        }
        base.OnTriggerEnter2D(col);
    }
}
